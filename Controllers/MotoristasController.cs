using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PraticaCargo.Api.Data;
using PraticaCargo.Api.Models;

namespace PraticaCargo.Api.Controllers;


[Authorize]
[ApiController]
[Route("api/[controller]")]

public class MotoristasController : ControllerBase
{
    private readonly AppDbContext _db;

    public MotoristasController(AppDbContext db)
    {
        _db = db;
    }

    // GET: api/motoristas
    [HttpGet]
    public async Task<ActionResult<List<Motorista>>> GetAll()
    {
        var list = await _db.Motoristas
            .OrderBy(m => m.Nome)
            .ToListAsync();

        return Ok(list);
    }

    // GET: api/motoristas/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Motorista>> GetById(int id)
    {
        var item = await _db.Motoristas.FindAsync(id);
        if (item is null) return NotFound();

        return Ok(item);
    }

    // GET: api/motoristas/by-cpf/123.456.789-00
    [HttpGet("by-cpf/{cpf}")]
    public async Task<ActionResult<Motorista>> GetByCpf(string cpf)
    {
        var item = await _db.Motoristas.FirstOrDefaultAsync(m => m.Cpf == cpf);
        if (item is null) return NotFound();

        return Ok(item);
    }

    // POST: api/motoristas
    [HttpPost]
    public async Task<ActionResult<Motorista>> Create([FromBody] Motorista dto)
    {
        // valida cpf único
        var exists = await _db.Motoristas.AnyAsync(m => m.Cpf == dto.Cpf);
        if (exists) return BadRequest("CPF já cadastrado.");

        _db.Motoristas.Add(dto);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    // PUT: api/motoristas/5
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] Motorista dto)
    {
        var item = await _db.Motoristas.FindAsync(id);
        if (item is null) return NotFound();

        // se cpf mudou, validar único
        if (item.Cpf != dto.Cpf)
        {
            var exists = await _db.Motoristas.AnyAsync(m => m.Cpf == dto.Cpf);
            if (exists) return BadRequest("CPF já cadastrado.");
        }

        item.Nome = dto.Nome;
        item.Cpf = dto.Cpf;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/motoristas/5
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var item = await _db.Motoristas.FindAsync(id);
        if (item is null) return NotFound();

        _db.Motoristas.Remove(item);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
