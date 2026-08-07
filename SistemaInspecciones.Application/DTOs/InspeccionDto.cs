namespace SistemaInspecciones.Application.DTOs
{
    public class InspeccionDto
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
        public List<FotografiaDto> Fotografias { get; set; } = new();
        public List<AudioDto> Audios { get; set; } = new();
        public List<ObservacionDto> Observaciones { get; set; } = new();
    }

    public class CrearInspeccionDto
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaInspeccion { get; set; }
        public decimal? Latitud { get; set; }
        public decimal? Longitud { get; set; }
        public string? DireccionReferencia { get; set; }
    }

    public class ActualizarInspeccionDto
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal? Latitud { get; set; }
        public decimal? Longitud { get; set; }
        public string? DireccionReferencia { get; set; }
    }

    public class ActualizarEstadoInspeccionDto
    {
        public string NuevoEstado { get; set; } = string.Empty;
    }
}