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
[Route("api/orcamento-despesa")]
public class OrcamentoDespesaController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    // Injeção de dependência do ApplicationDbContext
    public OrcamentoDespesaController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Listar todas as OrcamentoDespesas
    [HttpGet]
    public async Task<IActionResult> Listar(int? ano, int? unidadeGestoraId)
    {   
        // Cria a consulta inicial
        var query = _context.OrcamentoDespesas.AsQueryable();


        if (ano.HasValue){query = query.Where(p => p.Ano == ano.Value);}
        if (unidadeGestoraId.HasValue){query = query.Where(p => p.UnidadeGestora == unidadeGestoraId.Value);}

        var orcamentos = await query.ToListAsync();

        return Ok(orcamentos);  // Retorna todos os registros com o status 200
    }
    [HttpGet("progressao-mensal")]
    public async Task<IActionResult> ObterTotaisMensais(int ano, int unidadeGestoraId)
    {
         // Recupera as porcentagens para cada mês da tabela ProgramacaoFinanceiraDespesaConfig
        var config = await _context.ProgramacoesFinanceiras
            .Where(p => p.Ano == ano && p.UnidadeGestoraIDFK == unidadeGestoraId)
            .FirstOrDefaultAsync();

        // Calcula o total de OrcamentoDespesa para o ano e UnidadeGestora especificados
        var totalValorDespesa = await _context.OrcamentoDespesas
            .Where(o => o.Ano == ano && o.UnidadeGestora == unidadeGestoraId)
            .SumAsync(o => o.Valor);

        if (config == null)
        {   
            var resultados = new
            {
                Mes01Total = Math.Round(totalValorDespesa / 12, 2),
                Mes02Total = Math.Round(totalValorDespesa / 12, 2),
                Mes03Total = Math.Round(totalValorDespesa / 12, 2),
                Mes04Total = Math.Round(totalValorDespesa / 12, 2),
                Mes05Total = Math.Round(totalValorDespesa / 12, 2),
                Mes06Total = Math.Round(totalValorDespesa / 12, 2),
                Mes07Total = Math.Round(totalValorDespesa / 12, 2),
                Mes08Total = Math.Round(totalValorDespesa / 12, 2),
                Mes09Total = Math.Round(totalValorDespesa / 12, 2),
                Mes10Total = Math.Round(totalValorDespesa / 12, 2),
                Mes11Total = Math.Round(totalValorDespesa / 12, 2),
                Mes12Total = Math.Round(totalValorDespesa / 12, 2)+(totalValorDespesa-(12*Math.Round(totalValorDespesa / 12, 2))),
                totalValorDespesa = totalValorDespesa
            };

            return Ok(resultados);
        }
        // Arredonda os valores dos meses de 1 a 12
        var mes01Total = Math.Round(totalValorDespesa * (config.Mes01Perc/100), 2);
        var mes02Total = Math.Round(totalValorDespesa * (config.Mes02Perc/100), 2);
        var mes03Total = Math.Round(totalValorDespesa * (config.Mes03Perc/100), 2);
        var mes04Total = Math.Round(totalValorDespesa * (config.Mes04Perc/100), 2);
        var mes05Total = Math.Round(totalValorDespesa * (config.Mes05Perc/100), 2);
        var mes06Total = Math.Round(totalValorDespesa * (config.Mes06Perc/100), 2);
        var mes07Total = Math.Round(totalValorDespesa * (config.Mes07Perc/100), 2);
        var mes08Total = Math.Round(totalValorDespesa * (config.Mes08Perc/100), 2);
        var mes09Total = Math.Round(totalValorDespesa * (config.Mes09Perc/100), 2);
        var mes10Total = Math.Round(totalValorDespesa * (config.Mes10Perc/100), 2);
        var mes11Total = Math.Round(totalValorDespesa * (config.Mes11Perc/100), 2);
        var mes12Total = Math.Round(totalValorDespesa * (config.Mes12Perc/100), 2);

        // Soma dos valores arredondados dos meses 1 a 11
        var somaMeses = mes01Total + mes02Total + mes03Total + mes04Total + mes05Total + mes06Total + mes07Total + mes08Total + mes09Total + mes10Total + mes11Total + mes12Total;

        var resultadosComConfig = new
        {
            Mes01Total = mes01Total,
            Mes02Total = mes02Total,
            Mes03Total = mes03Total,
            Mes04Total = mes04Total,
            Mes05Total = mes05Total,
            Mes06Total = mes06Total,
            Mes07Total = mes07Total,
            Mes08Total = mes08Total,
            Mes09Total = mes09Total,
            Mes10Total = mes10Total,
            Mes11Total = mes11Total,
            Mes12Total = mes12Total+(totalValorDespesa-somaMeses),
            totalValorDespesa = totalValorDespesa
        };

        return Ok(resultadosComConfig);
    }
}
