using SistemaInspecciones.Application.DTOs;
using SistemaInspecciones.Application.Interfaces.Repositories;
using SistemaInspecciones.Application.Interfaces.Services;
using SistemaInspecciones.Domain.Entities;
using SistemaInspecciones.Domain.Enums;

namespace SistemaInspecciones.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return usuarios.Select(MapToDto);
        }

        public async Task<UsuarioDto?> GetByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            return usuario is null ? null : MapToDto(usuario);
        }

        public async Task<UsuarioDto> CreateAsync(CrearUsuarioDto dto)
        {
            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                PasswordHash = PasswordHasher.Hash(dto.Password),
                Rol = Enum.Parse<RolUsuario>(dto.Rol, ignoreCase: true),
                Estado = true,
                FechaCreacion = DateTime.UtcNow
            };

            await _usuarioRepository.AddAsync(usuario);
            await _usuarioRepository.SaveChangesAsync();

            return MapToDto(usuario);
        }

        public async Task<bool> UpdateAsync(int id, ActualizarUsuarioDto dto)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario is null) return false;

            usuario.Nombre = dto.Nombre;
            usuario.Rol = Enum.Parse<RolUsuario>(dto.Rol, ignoreCase: true);
            usuario.Estado = dto.Estado;

            _usuarioRepository.Update(usuario);
            return await _usuarioRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario is null) return false;

            _usuarioRepository.Delete(usuario);
            return await _usuarioRepository.SaveChangesAsync();
        }

        private static UsuarioDto MapToDto(Usuario u) => new()
        {
            Id = u.Id,
            Nombre = u.Nombre,
            Correo = u.Correo,
            Rol = u.Rol.ToString(),
            Estado = u.Estado,
            FechaCreacion = u.FechaCreacion,
            UltimoAcceso = u.UltimoAcceso
        };
    }
}