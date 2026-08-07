using SistemaInspecciones.Domain.Entities;

namespace SistemaInspecciones.Application.Interfaces.Repositories
{
    public interface IInspeccionRepository : IGenericRepository<Inspeccion>
    {
        Task<Inspeccion?> GetByIdConDetalleAsync(int id);
        Task<IEnumerable<Inspeccion>> GetByUsuarioIdAsync(int usuarioId);
    }
}