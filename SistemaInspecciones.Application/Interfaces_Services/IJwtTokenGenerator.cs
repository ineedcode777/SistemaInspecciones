using SistemaInspecciones.Domain.Entities;

namespace SistemaInspecciones.Application.Interfaces.Services
{
    public interface IJwtTokenGenerator
    {
        (string Token, DateTime Expiracion) GenerarToken(Usuario usuario);
    }
}