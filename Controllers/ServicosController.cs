using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PraticaCargo.Api.Data;
using PraticaCargo.Api.Models;

namespace PraticaCargo.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]


public class ServicosController : ControllerBase
{
    private readonly AppDbContext _ctx;

    public ServicosController(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    [HttpGet]
    public async Task<ActionResult<List<Servico>>> GetAll()
    {
        var list = await _ctx.Servicos
            .AsNoTracking()
            .Include(s => s.Empresa)
            .Include(s => s.Motorista)
            .ToListAsync();

        return Ok(list);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Servico>> GetById(int id)
    {
        var servico = await _ctx.Servicos
            .AsNoTracking()
            .Include(s => s.Empresa)
            .Include(s => s.Motorista)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (servico is null) return NotFound();
        return Ok(servico);
    }

    [HttpGet("by-empresa/{empresaId:int}")]
    public async Task<ActionResult<List<Servico>>> GetByEmpresa(int empresaId)
    {
        var list = await _ctx.Servicos
            .AsNoTracking()
            .Where(s => s.EmpresaId == empresaId)
            .Include(s => s.Empresa)
            .Include(s => s.Motorista)
            .ToListAsync();

        return Ok(list);
    }

    [HttpGet("by-motorista/{motoristaId:int}")]
    public async Task<ActionResult<List<Servico>>> GetByMotorista(int motoristaId)
    {
        var list = await _ctx.Servicos
            .AsNoTracking()
            .Where(s => s.MotoristaId == motoristaId)
            .Include(s => s.Empresa)
            .Include(s => s.Motorista)
            .ToListAsync();

        return Ok(list);
    }


    [HttpPost]
    public async Task<ActionResult<Servico>> Create(Servico data)
    {
        // regra: precisa existir empresa e motorista
        var empresaExists = await _ctx.Empresas.AnyAsync(e => e.Id == data.EmpresaId);
        if (!empresaExists) return BadRequest("EmpresaId inválido.");

        var motoristaExists = await _ctx.Motoristas.AnyAsync(m => m.Id == data.MotoristaId);
        if (!motoristaExists) return BadRequest("MotoristaId inválido.");

        if (data.Data == default) return BadRequest("Data é obrigatória.");

        _ctx.Servicos.Add(data);
        await _ctx.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = data.Id }, data);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Servico data)
    {
        if (id != data.Id) return BadRequest("Id da rota diferente do body.");

        var current = await _ctx.Servicos.FirstOrDefaultAsync(x => x.Id == id);
        if (current is null) return NotFound();

        var empresaExists = await _ctx.Empresas.AnyAsync(e => e.Id == data.EmpresaId);
        if (!empresaExists) return BadRequest("EmpresaId inválido.");

        var motoristaExists = await _ctx.Motoristas.AnyAsync(m => m.Id == data.MotoristaId);
        if (!motoristaExists) return BadRequest("MotoristaId inválido.");

        current.EmpresaId = data.EmpresaId;
        current.MotoristaId = data.MotoristaId;
        current.Carga = data.Carga;
        current.Peso = data.Peso;
        current.QuemRetira = data.QuemRetira;
        current.Data = data.Data;
        current.Tipo = data.Tipo;

        await _ctx.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var current = await _ctx.Servicos.FirstOrDefaultAsync(x => x.Id == id);
        if (current is null) return NotFound();

        _ctx.Servicos.Remove(current);
        await _ctx.SaveChangesAsync();
        return NoContent();
    }
}
