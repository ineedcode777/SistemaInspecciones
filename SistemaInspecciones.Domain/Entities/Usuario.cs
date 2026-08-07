using SistemaInspecciones.Domain.Enums;

namespace SistemaInspecciones.Domain.Entities
{
    public class Usuario : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public RolUsuario Rol { get; set; }
        public bool Estado { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime? UltimoAcceso { get; set; }

        // Relaciones
        public ICollection<Inspeccion> Inspecciones { get; set; } = new List<Inspeccion>();
        public ICollection<Observacion> Observaciones { get; set; } = new List<Observacion>();
        public ICollection<HistorialInspeccion> HistorialInspecciones { get; set; } = new List<HistorialInspeccion>();
    }
}