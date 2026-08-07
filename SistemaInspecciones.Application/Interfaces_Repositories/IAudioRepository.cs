using SistemaInspecciones.Domain.Entities;

namespace SistemaInspecciones.Application.Interfaces.Repositories
{
    public interface IAudioRepository : IGenericRepository<Audio>
    {
        Task<IEnumerable<Audio>> GetByInspeccionIdAsync(int inspeccionId);
    }
}