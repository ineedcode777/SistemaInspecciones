using Microsoft.EntityFrameworkCore;
using SistemaInspecciones.Application.Interfaces.Repositories;
using SistemaInspecciones.Domain.Entities;
using SistemaInspecciones.Infrastructure.Data;

namespace SistemaInspecciones.Infrastructure.Repositories
{
    public class InspeccionRepository : GenericRepository<Inspeccion>, IInspeccionRepository
    {
        public InspeccionRepository(AppDbContext context) : base(context) { }

        public async Task<Inspeccion?> GetByIdConDetalleAsync(int id) =>
            await _dbSet
                .Include(i => i.Usuario)
                .Include(i => i.Fotografias)
                .Include(i => i.Audios)
                .Include(i => i.Observaciones)
                .FirstOrDefaultAsync(i => i.Id == id);

        public async Task<IEnumerable<Inspeccion>> GetByUsuarioIdAsync(int usuarioId) =>
            await _dbSet.Where(i => i.UsuarioId == usuarioId).ToListAsync();
    }
}