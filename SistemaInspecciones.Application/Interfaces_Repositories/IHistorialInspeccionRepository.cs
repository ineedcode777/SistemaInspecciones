using SistemaInspecciones.Domain.Entities;

namespace SistemaInspecciones.Application.Interfaces.Repositories
{
    public interface IHistorialInspeccionRepository : IGenericRepository<HistorialInspeccion>
    {
        Task<IEnumerable<HistorialInspeccion>> GetByInspeccionIdAsync(int inspeccionId);
    }
}