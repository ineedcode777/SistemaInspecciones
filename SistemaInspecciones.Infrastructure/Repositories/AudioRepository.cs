using Microsoft.EntityFrameworkCore;
using SistemaInspecciones.Application.Interfaces.Repositories;
using SistemaInspecciones.Domain.Entities;
using SistemaInspecciones.Infrastructure.Data;

namespace SistemaInspecciones.Infrastructure.Repositories
{
    public class AudioRepository : GenericRepository<Audio>, IAudioRepository
    {
        public AudioRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Audio>> GetByInspeccionIdAsync(int inspeccionId) =>
            await _dbSet.Where(a => a.InspeccionId == inspeccionId).ToListAsync();
    }
}