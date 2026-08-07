using SistemaInspecciones.Application.DTOs;

namespace SistemaInspecciones.Application.Interfaces.Services
{
    public interface IInspeccionService
    {
        Task<IEnumerable<InspeccionDto>> GetAllAsync();
        Task<InspeccionDto?> GetByIdAsync(int id);
        Task<IEnumerable<InspeccionDto>> GetByUsuarioAsync(int usuarioId);
        Task<InspeccionDto> CreateAsync(int usuarioId, CrearInspeccionDto dto);
        Task<bool> UpdateAsync(int id, ActualizarInspeccionDto dto);
        Task<bool> CambiarEstadoAsync(int id, int usuarioId, ActualizarEstadoInspeccionDto dto);
        Task<bool> DeleteAsync(int id);
    }
}