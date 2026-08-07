using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaInspecciones.Domain.Entities;

namespace SistemaInspecciones.Infrastructure.Data.Configurations
{
    public class InspeccionConfiguration : IEntityTypeConfiguration<Inspeccion>
    {
        public void Configure(EntityTypeBuilder<Inspeccion> builder)
        {
            builder.ToTable("Inspecciones");

            builder.Property(i => i.Titulo).HasMaxLength(150).IsRequired();
            builder.Property(i => i.Descripcion).HasColumnType("text").IsRequired();
            builder.Property(i => i.Latitud).HasColumnType("decimal(10,8)");
            builder.Property(i => i.Longitud).HasColumnType("decimal(11,8)");
            builder.Property(i => i.DireccionReferencia).HasMaxLength(250);
            builder.Property(i => i.Estado).HasConversion<string>().HasMaxLength(30).IsRequired();

            builder.HasMany(i => i.Fotografias)
                .WithOne(f => f.Inspeccion)
                .HasForeignKey(f => f.InspeccionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(i => i.Audios)
                .WithOne(a => a.Inspeccion)
                .HasForeignKey(a => a.InspeccionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(i => i.Observaciones)
                .WithOne(o => o.Inspeccion)
                .HasForeignKey(o => o.InspeccionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(i => i.HistorialInspecciones)
                .WithOne(h => h.Inspeccion)
                .HasForeignKey(h => h.InspeccionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}