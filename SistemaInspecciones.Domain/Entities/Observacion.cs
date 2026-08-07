namespace SistemaInspecciones.Domain.Entities
{
    public class Observacion : BaseEntity
    {
        public int InspeccionId { get; set; }
        public Inspeccion? Inspeccion { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public string Comentario { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}