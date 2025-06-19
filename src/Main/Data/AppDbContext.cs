using Microsoft.EntityFrameworkCore;
using Fase4Cap7WebserviceASPNET.Main.Models;

namespace Fase4Cap7WebserviceASPNET.Main.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Ocorrencia> Ocorrencias { get; set; } = null!;
        public DbSet<TipoImpacto> TiposImpacto { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ocorrencia>()
                .HasOne(o => o.TipoImpacto)
                .WithMany()
                .HasForeignKey(o => o.TipoImpactoId)
                .OnDelete(DeleteBehavior.Restrict); // ou .Cascade se quiser deletar o TipoImpacto com a Ocorrência (não recomendado)

            base.OnModelCreating(modelBuilder);
        }
    }
}
