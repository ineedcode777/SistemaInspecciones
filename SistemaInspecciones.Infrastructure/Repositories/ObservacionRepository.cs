using Microsoft.EntityFrameworkCore;
using SistemaInspecciones.Application.Interfaces.Repositories;
using SistemaInspecciones.Domain.Entities;
using SistemaInspecciones.Infrastructure.Data;

namespace SistemaInspecciones.Infrastructure.Repositories
{
    public class ObservacionRepository : GenericRepository<Observacion>, IObservacionRepository
    {
        public ObservacionRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Observacion>> GetByInspeccionIdAsync(int inspeccionId) =>
            await _dbSet.Where(o => o.InspeccionId == inspeccionId).ToListAsync();
    }
}