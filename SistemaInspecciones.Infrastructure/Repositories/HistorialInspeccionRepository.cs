using Microsoft.EntityFrameworkCore;
using SistemaInspecciones.Application.Interfaces.Repositories;
using SistemaInspecciones.Domain.Entities;
using SistemaInspecciones.Infrastructure.Data;

namespace SistemaInspecciones.Infrastructure.Repositories
{
    public class HistorialInspeccionRepository : GenericRepository<HistorialInspeccion>, IHistorialInspeccionRepository
    {
        public HistorialInspeccionRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<HistorialInspeccion>> GetByInspeccionIdAsync(int inspeccionId) =>
            await _dbSet.Where(h => h.InspeccionId == inspeccionId).ToListAsync();
    }
}