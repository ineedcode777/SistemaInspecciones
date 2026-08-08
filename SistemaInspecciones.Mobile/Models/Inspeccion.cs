

namespace SistemaInspecciones.Mobile.Models
{
    public class Inspeccion
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string NombreTecnico { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaInspeccion { get; set; }
        public decimal? Latitud { get; set; }
        public decimal? Longitud { get; set; }
        public string? DireccionReferencia { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public List<Fotografia> Fotografias { get; set; } = new();
        public List<Audio> Audios { get; set; } = new();
        public List<Observacion> Observaciones { get; set; } = new();
    }

    public class CrearInspeccionRequest
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaInspeccion { get; set; }
        public decimal? Latitud { get; set; }
        public decimal? Longitud { get; set; }
        public string? DireccionReferencia { get; set; }
    }
}