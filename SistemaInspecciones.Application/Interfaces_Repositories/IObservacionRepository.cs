using SistemaInspecciones.Domain.Entities;

namespace SistemaInspecciones.Application.Interfaces.Repositories
{
    public interface IObservacionRepository : IGenericRepository<Observacion>
    {
        Task<IEnumerable<Observacion>> GetByInspeccionIdAsync(int inspeccionId);
    }
}