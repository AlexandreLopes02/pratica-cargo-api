using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PraticaCargo.Api.Data;
using PraticaCargo.Api.Models;

namespace PraticaCargo.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]


public class EmpresasController : ControllerBase
{
    private readonly AppDbContext _ctx;

    public EmpresasController(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    [HttpGet]
    public async Task<ActionResult<List<Empresa>>> GetAll()
    {
        var list = await _ctx.Empresas.AsNoTracking().ToListAsync();
        return Ok(list);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Empresa>> GetById(int id)
    {
        var empresa = await _ctx.Empresas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (empresa is null) return NotFound();
        return Ok(empresa);
    }

    [HttpGet("by-cnpj/{cnpj}")]
    public async Task<ActionResult<Empresa>> GetByCnpj(string cnpj)
    {
        var empresa = await _ctx.Empresas.AsNoTracking().FirstOrDefaultAsync(x => x.Cnpj == cnpj);
        if (empresa is null) return NotFound();
        return Ok(empresa);
    }

    [HttpPost]
    public async Task<ActionResult<Empresa>> Create(Empresa data)
    {
        var exists = await _ctx.Empresas.AnyAsync(x => x.Cnpj == data.Cnpj);
        if (exists) return BadRequest("CNPJ já cadastrado.");

        data.Endereco = data.Endereco?.Trim() ?? "";

        _ctx.Empresas.Add(data);
        await _ctx.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = data.Id }, data);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Empresa data)
    {
        if (id != data.Id) return BadRequest("Id da rota diferente do body.");

        var existsOther = await _ctx.Empresas.AnyAsync(x => x.Cnpj == data.Cnpj && x.Id != id);
        if (existsOther) return BadRequest("CNPJ já cadastrado.");

        var current = await _ctx.Empresas.FirstOrDefaultAsync(x => x.Id == id);
        if (current is null) return NotFound();

        current.Nome = data.Nome;
        current.Cnpj = data.Cnpj;
        current.Endereco = data.Endereco;

        await _ctx.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var current = await _ctx.Empresas.FirstOrDefaultAsync(x => x.Id == id);
        if (current is null) return NotFound();

        _ctx.Empresas.Remove(current);
        await _ctx.SaveChangesAsync();
        return NoContent();
    }
}
