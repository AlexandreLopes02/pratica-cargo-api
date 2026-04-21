using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PraticaCargo.Api.Data;
using PraticaCargo.Api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PraticaCargo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]


public class AuthController : ControllerBase
{
    private readonly AppDbContext _ctx;
    private readonly IConfiguration _config;

    public AuthController(AppDbContext ctx, IConfiguration config)
    {
        _ctx = ctx;
        _config = config;
    }

    public record RegisterRequest(string Name, string Email, string Password);
    public record LoginRequest(string Name, string Password);

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterRequest req)
    {
        var emailExists = await _ctx.Users.AnyAsync(u => u.Email == req.Email);
        if (emailExists) return BadRequest("Email já cadastrado.");

        var user = new User
        {
            Name = req.Name,
            Email = req.Email,
            Password = req.Password
        };

        _ctx.Users.Add(user);
        await _ctx.SaveChangesAsync();

        return Ok(new { message = "Usuário criado com sucesso" });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequest req)
    {
        var login = req.Name.Trim();
        var user = await _ctx.Users.FirstOrDefaultAsync(u =>
            (u.Name == login || u.Email == login) &&
            u.Password == req.Password);

        if (user is null) return Unauthorized("Usuário ou senha inválidos.");

        var token = GenerateJwt(user);
        return Ok(new
        {
            token,
            user = new { user.Id, user.Name, user.Email }
        });
    }

    private string GenerateJwt(User user)
    {
        var key = _config["Jwt:Key"]!;
        var issuer = _config["Jwt:Issuer"]!;
        var audience = _config["Jwt:Audience"]!;

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim("name", user.Name),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
        };

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
