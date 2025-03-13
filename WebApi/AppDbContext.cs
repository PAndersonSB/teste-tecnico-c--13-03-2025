using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<OrcamentoDespesa> OrcamentoDespesas { get; set; }
        public DbSet<ProgramacaoFinanceiraDespesaConfig> ProgramacoesFinanceiras { get; set; }

    }
}
