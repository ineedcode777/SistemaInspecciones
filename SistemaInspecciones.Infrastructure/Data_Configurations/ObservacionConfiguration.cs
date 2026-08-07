using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaInspecciones.Domain.Entities;

namespace SistemaInspecciones.Infrastructure.Data.Configurations
{
    public class ObservacionConfiguration : IEntityTypeConfiguration<Observacion>
    {
        public void Configure(EntityTypeBuilder<Observacion> builder)
        {
            builder.ToTable("Observaciones");

            builder.Property(o => o.Comentario).HasColumnType("text").IsRequired();
        }
    }
}