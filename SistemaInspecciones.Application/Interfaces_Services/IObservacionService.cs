using SistemaInspecciones.Application.DTOs;

namespace SistemaInspecciones.Application.Interfaces.Services
{
    public interface IObservacionService
    {
        Task<IEnumerable<ObservacionDto>> GetByInspeccionIdAsync(int inspeccionId);
        Task<ObservacionDto> CreateAsync(int inspeccionId, int usuarioId, CrearObservacionDto dto);
        Task<bool> UpdateAsync(int id, CrearObservacionDto dto);
        Task<bool> DeleteAsync(int id);
    }
}