using Microsoft.EntityFrameworkCore;
using SistemaInspecciones.Domain.Entities;

namespace SistemaInspecciones.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Inspeccion> Inspecciones => Set<Inspeccion>();
        public DbSet<Fotografia> Fotografias => Set<Fotografia>();
        public DbSet<Audio> Audios => Set<Audio>();
        public DbSet<Observacion> Observaciones => Set<Observacion>();
        public DbSet<HistorialInspeccion> HistorialInspecciones => Set<HistorialInspeccion>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}