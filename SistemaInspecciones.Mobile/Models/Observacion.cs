namespace SistemaInspecciones.Mobile.Models
{
    public class Observacion
    {
        public int Id { get; set; }
        public int InspeccionId { get; set; }
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Comentario { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
    }

    public class CrearObservacionRequest
    {
        public string Comentario { get; set; } = string.Empty;
    }
}