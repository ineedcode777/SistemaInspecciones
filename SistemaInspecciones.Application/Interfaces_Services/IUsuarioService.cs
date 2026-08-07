using SistemaInspecciones.Application.DTOs;

namespace SistemaInspecciones.Application.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDto>> GetAllAsync();
        Task<UsuarioDto?> GetByIdAsync(int id);
        Task<UsuarioDto> CreateAsync(CrearUsuarioDto dto);
        Task<bool> UpdateAsync(int id, ActualizarUsuarioDto dto);
        Task<bool> DeleteAsync(int id);
    }
}