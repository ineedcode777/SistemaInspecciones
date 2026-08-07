namespace SistemaInspecciones.Application.DTOs
{
    public class HistorialInspeccionDto
    {
        public int Id { get; set; }
        public int InspeccionId { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Accion { get; set; } = string.Empty;
        public string? EstadoAnterior { get; set; }
        public string? EstadoNuevo { get; set; }
        public string? Detalle { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}