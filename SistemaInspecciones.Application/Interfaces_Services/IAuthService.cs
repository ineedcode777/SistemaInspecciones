using SistemaInspecciones.Application.DTOs;

namespace SistemaInspecciones.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);
    }
}