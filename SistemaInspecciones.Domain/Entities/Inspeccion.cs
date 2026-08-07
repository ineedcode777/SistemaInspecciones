using SistemaInspecciones.Domain.Enums;

namespace SistemaInspecciones.Domain.Entities
{
    public class Inspeccion : BaseEntity
    {
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaInspeccion { get; set; }

        public decimal? Latitud { get; set; }
        public decimal? Longitud { get; set; }
        public string? DireccionReferencia { get; set; }

        public EstadoInspeccion Estado { get; set; } = EstadoInspeccion.Borrador;

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime? FechaActualizacion { get; set; }

        // Relaciones
        public ICollection<Fotografia> Fotografias { get; set; } = new List<Fotografia>();
        public ICollection<Audio> Audios { get; set; } = new List<Audio>();
        public ICollection<Observacion> Observaciones { get; set; } = new List<Observacion>();
        public ICollection<HistorialInspeccion> HistorialInspecciones { get; set; } = new List<HistorialInspeccion>();
    }
}