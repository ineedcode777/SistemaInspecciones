using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaInspecciones.Domain.Entities;

namespace SistemaInspecciones.Infrastructure.Data.Configurations
{
    public class AudioConfiguration : IEntityTypeConfiguration<Audio>
    {
        public void Configure(EntityTypeBuilder<Audio> builder)
        {
            builder.ToTable("Audios");

            builder.Property(a => a.NombreArchivo).HasMaxLength(200).IsRequired();
            builder.Property(a => a.RutaArchivo).HasMaxLength(500).IsRequired();
            builder.Property(a => a.TipoContenido).HasMaxLength(100).IsRequired();
        }
    }
}