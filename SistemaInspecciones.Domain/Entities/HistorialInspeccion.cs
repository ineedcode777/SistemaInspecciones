using SistemaInspecciones.Domain.Enums;

namespace SistemaInspecciones.Domain.Entities
{
    public class HistorialInspeccion : BaseEntity
    {
        public int InspeccionId { get; set; }
        public Inspeccion? Inspeccion { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public string Accion { get; set; } = string.Empty;
        public EstadoInspeccion? EstadoAnterior { get; set; }
        public EstadoInspeccion? EstadoNuevo { get; set; }
        public string? Detalle { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}