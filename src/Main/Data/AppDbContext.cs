using Microsoft.EntityFrameworkCore;
using Fase4Cap7WebserviceASPNET.Main.Models;

namespace Fase4Cap7WebserviceASPNET.Main.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Ocorrencia> Ocorrencias { get; set; } = null!;
        public DbSet<TipoImpacto> TiposImpacto { get; set; } = null!;
    }
}
