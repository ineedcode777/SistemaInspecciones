namespace SistemaInspecciones.Domain.Entities
{
    public class Audio : BaseEntity
    {
        public int InspeccionId { get; set; }
        public Inspeccion? Inspeccion { get; set; }

        public string NombreArchivo { get; set; } = string.Empty;
        public string RutaArchivo { get; set; } = string.Empty;
        public string TipoContenido { get; set; } = string.Empty;
        public long TamanoBytes { get; set; }
        public int? DuracionSegundos { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}