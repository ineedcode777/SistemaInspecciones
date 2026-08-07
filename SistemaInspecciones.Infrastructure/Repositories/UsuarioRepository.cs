using Microsoft.EntityFrameworkCore;
using SistemaInspecciones.Application.Interfaces.Repositories;
using SistemaInspecciones.Domain.Entities;
using SistemaInspecciones.Infrastructure.Data;

namespace SistemaInspecciones.Infrastructure.Repositories
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext context) : base(context) { }

        public async Task<Usuario?> GetByCorreoAsync(string correo) =>
            await _dbSet.FirstOrDefaultAsync(u => u.Correo == correo);
    }
}