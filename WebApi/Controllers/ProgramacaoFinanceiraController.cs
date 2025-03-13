using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[ApiController]
[Route("api/programacao-financeira")]
public class ProgramacaoFinanceiraController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProgramacaoFinanceiraController(ApplicationDbContext context)
    {
        _context = context;
    }

    // 1. Cadastrar Programação Financeira
    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] ProgramacaoFinanceiraDespesaConfig config)
    {
        // Verifica se já existe programação para o mesmo Ano e UnidadeGestora
        bool existe = await _context.ProgramacoesFinanceiras
            .AnyAsync(p => p.Ano == config.Ano && p.UnidadeGestoraIDFK == config.UnidadeGestoraIDFK);

        if (existe)
            return BadRequest("Já existe uma programação para este Ano e Unidade Gestora.");

        _context.ProgramacoesFinanceiras.Add(config);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Consultar), new { id = config.ProgramacaoFinanceiraDespesaConfigID }, config);
    }

    // 2. Editar Programação Financeira
    [HttpPut("{id}")]
    public async Task<IActionResult> Editar(int id, [FromBody] ProgramacaoFinanceiraDespesaConfig config)
    {
        if (id != config.ProgramacaoFinanceiraDespesaConfigID)
            return BadRequest("ID inválido.");

        _context.Entry(config).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // 3. Excluir Programação Financeira
    [HttpDelete("{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var config = await _context.ProgramacoesFinanceiras.FindAsync(id);
        if (config == null)
            return NotFound();

        _context.ProgramacoesFinanceiras.Remove(config);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    // 5. Consultar Programação Financeira por ID
    [HttpGet("{id}")]
    public async Task<IActionResult> Consultar(int id)
    {
        var config = await _context.ProgramacoesFinanceiras.FindAsync(id);
        if (config == null)
            return NotFound();

        return Ok(config);
    }

    // 6. Listar todas as programações
    [HttpGet]
    public async Task<IActionResult> Listar(int? ano, int? unidadeGestoraId)
    {
        // Cria a consulta inicial
        var query = _context.ProgramacoesFinanceiras.AsQueryable();

        // Aplica filtro por Ano se fornecido
        if (ano.HasValue)
        {
            query = query.Where(p => p.Ano == ano.Value);
        }

        // Aplica filtro por UnidadeGestoraIDFK se fornecido
        if (unidadeGestoraId.HasValue)
        {
            query = query.Where(p => p.UnidadeGestoraIDFK == unidadeGestoraId.Value);
        }

        // Obtém os resultados filtrados
        var programacoes = await query.ToListAsync();

        return Ok(programacoes);
    }
}
