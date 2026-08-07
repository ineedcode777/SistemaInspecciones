using SistemaInspecciones.Domain.Entities;

namespace SistemaInspecciones.Application.Interfaces.Repositories
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Task<Usuario?> GetByCorreoAsync(string correo);
    }
}