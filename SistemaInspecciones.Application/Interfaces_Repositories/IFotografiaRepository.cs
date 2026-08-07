using SistemaInspecciones.Domain.Entities;

namespace SistemaInspecciones.Application.Interfaces.Repositories
{
    public interface IFotografiaRepository : IGenericRepository<Fotografia>
    {
        Task<IEnumerable<Fotografia>> GetByInspeccionIdAsync(int inspeccionId);
    }
}