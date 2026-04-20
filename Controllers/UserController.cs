using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PraticaCargo.Api.Data;
using PraticaCargo.Api.Models;

namespace PraticaCargo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _db;
    public UsersController(AppDbContext db) => _db = db;

    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetById(int id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user is null) return NotFound();
        return Ok(user);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] User dto)
    {
        var user = await _db.Users.FindAsync(id);
        if (user is null) return NotFound();
        if (!string.IsNullOrWhiteSpace(dto.Password))
            user.Password = dto.Password;


        // atualiza somente campos permitidos
        user.Name = dto.Name;
        user.Email = dto.Email;

        await _db.SaveChangesAsync();

        return Ok(new { user.Id, user.Name, user.Email });
    }
}
