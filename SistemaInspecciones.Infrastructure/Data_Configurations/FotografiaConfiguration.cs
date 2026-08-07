using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaInspecciones.Domain.Entities;

namespace SistemaInspecciones.Infrastructure.Data.Configurations
{
    public class FotografiaConfiguration : IEntityTypeConfiguration<Fotografia>
    {
        public void Configure(EntityTypeBuilder<Fotografia> builder)
        {
            builder.ToTable("Fotografias");

            builder.Property(f => f.NombreArchivo).HasMaxLength(200).IsRequired();
            builder.Property(f => f.RutaArchivo).HasMaxLength(500).IsRequired();
            builder.Property(f => f.TipoContenido).HasMaxLength(100).IsRequired();
            builder.Property(f => f.Descripcion).HasMaxLength(250);
        }
    }
}