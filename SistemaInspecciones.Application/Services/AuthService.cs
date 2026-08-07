using SistemaInspecciones.Application.DTOs;
using SistemaInspecciones.Application.Interfaces.Repositories;
using SistemaInspecciones.Application.Interfaces.Services;

namespace SistemaInspecciones.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(IUsuarioRepository usuarioRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _usuarioRepository = usuarioRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
        {
            var usuario = await _usuarioRepository.GetByCorreoAsync(request.Correo);

            if (usuario is null || !usuario.Estado)
                return null;

            if (!PasswordHasher.Verify(request.Password, usuario.PasswordHash))
                return null;

            usuario.UltimoAcceso = DateTime.UtcNow;
            _usuarioRepository.Update(usuario);
            await _usuarioRepository.SaveChangesAsync();

            var (token, expiracion) = _jwtTokenGenerator.GenerarToken(usuario);

            return new LoginResponseDto
            {
                Token = token,
                UsuarioId = usuario.Id,
                Nombre = usuario.Nombre,
                Rol = usuario.Rol.ToString(),
                FechaExpiracion = expiracion
            };
        }
    }
}