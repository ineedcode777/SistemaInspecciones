using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaInspecciones.Domain.Entities;

namespace SistemaInspecciones.Infrastructure.Data.Configurations
{
    public class HistorialInspeccionConfiguration : IEntityTypeConfiguration<HistorialInspeccion>
    {
        public void Configure(EntityTypeBuilder<HistorialInspeccion> builder)
        {
            builder.ToTable("HistorialInspecciones");

            builder.Property(h => h.Accion).HasMaxLength(100).IsRequired();
            builder.Property(h => h.EstadoAnterior).HasConversion<string>().HasMaxLength(30);
            builder.Property(h => h.EstadoNuevo).HasConversion<string>().HasMaxLength(30);
            builder.Property(h => h.Detalle).HasColumnType("text");
        }
    }
}