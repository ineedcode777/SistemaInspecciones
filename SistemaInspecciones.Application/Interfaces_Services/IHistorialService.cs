using SistemaInspecciones.Application.DTOs;

namespace SistemaInspecciones.Application.Interfaces.Services
{
    public interface IHistorialService
    {
        Task<IEnumerable<HistorialInspeccionDto>> GetByInspeccionIdAsync(int inspeccionId);
        Task RegistrarEventoAsync(int inspeccionId, int usuarioId, string accion, string? estadoAnterior, string? estadoNuevo, string? detalle);
    }
}