using Microsoft.EntityFrameworkCore;
using SistemaInspecciones.Application.Interfaces.Repositories;
using SistemaInspecciones.Domain.Entities;
using SistemaInspecciones.Infrastructure.Data;

namespace SistemaInspecciones.Infrastructure.Repositories
{
    public class FotografiaRepository : GenericRepository<Fotografia>, IFotografiaRepository
    {
        public FotografiaRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Fotografia>> GetByInspeccionIdAsync(int inspeccionId) =>
            await _dbSet.Where(f => f.InspeccionId == inspeccionId).ToListAsync();
    }
}