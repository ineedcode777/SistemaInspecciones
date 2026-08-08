namespace SistemaInspecciones.Mobile.Models
{
    public class Fotografia
    {
        public int Id { get; set; }
        public int InspeccionId { get; set; }
        public string NombreArchivo { get; set; } = string.Empty;
        public string RutaArchivo { get; set; } = string.Empty;
        public string TipoContenido { get; set; } = string.Empty;
        public long TamanoBytes { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}