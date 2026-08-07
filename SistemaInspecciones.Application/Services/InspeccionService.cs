using SistemaInspecciones.Application.DTOs;
using SistemaInspecciones.Application.Interfaces.Repositories;
using SistemaInspecciones.Application.Interfaces.Services;
using SistemaInspecciones.Domain.Entities;
using SistemaInspecciones.Domain.Enums;

namespace SistemaInspecciones.Application.Services
{
    public class InspeccionService : IInspeccionService
    {
        private readonly IInspeccionRepository _inspeccionRepository;
        private readonly IHistorialService _historialService;

        public InspeccionService(IInspeccionRepository inspeccionRepository, IHistorialService historialService)
        {
            _inspeccionRepository = inspeccionRepository;
            _historialService = historialService;
        }

        public async Task<IEnumerable<InspeccionDto>> GetAllAsync()
        {
            var inspecciones = await _inspeccionRepository.GetAllAsync();
            return inspecciones.Select(MapToDto);
        }

        public async Task<InspeccionDto?> GetByIdAsync(int id)
        {
            var inspeccion = await _inspeccionRepository.GetByIdConDetalleAsync(id);
            return inspeccion is null ? null : MapToDto(inspeccion);
        }

        public async Task<IEnumerable<InspeccionDto>> GetByUsuarioAsync(int usuarioId)
        {
            var inspecciones = await _inspeccionRepository.GetByUsuarioIdAsync(usuarioId);
            return inspecciones.Select(MapToDto);
        }

        public async Task<InspeccionDto> CreateAsync(int usuarioId, CrearInspeccionDto dto)
        {
            var inspeccion = new Inspeccion
            {
                UsuarioId = usuarioId,
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                FechaInspeccion = dto.FechaInspeccion,
                Latitud = dto.Latitud,
                Longitud = dto.Longitud,
                DireccionReferencia = dto.DireccionReferencia,
                Estado = EstadoInspeccion.Borrador,
                FechaCreacion = DateTime.UtcNow
            };

            await _inspeccionRepository.AddAsync(inspeccion);
            await _inspeccionRepository.SaveChangesAsync();

            await _historialService.RegistrarEventoAsync(inspeccion.Id, usuarioId, "Inspeccion creada", null, EstadoInspeccion.Borrador.ToString(), null);

            return MapToDto(inspeccion);
        }

        public async Task<bool> UpdateAsync(int id, ActualizarInspeccionDto dto)
        {
            var inspeccion = await _inspeccionRepository.GetByIdAsync(id);
            if (inspeccion is null) return false;

            if (inspeccion.Estado == EstadoInspeccion.Cerrada)
                return false; // Regla de negocio: inspección cerrada no puede editarse

            inspeccion.Titulo = dto.Titulo;
            inspeccion.Descripcion = dto.Descripcion;
            inspeccion.Latitud = dto.Latitud;
            inspeccion.Longitud = dto.Longitud;
            inspeccion.DireccionReferencia = dto.DireccionReferencia;
            inspeccion.FechaActualizacion = DateTime.UtcNow;

            _inspeccionRepository.Update(inspeccion);
            return await _inspeccionRepository.SaveChangesAsync();
        }

        public async Task<bool> CambiarEstadoAsync(int id, int usuarioId, ActualizarEstadoInspeccionDto dto)
        {
            var inspeccion = await _inspeccionRepository.GetByIdAsync(id);
            if (inspeccion is null) return false;

            var estadoAnterior = inspeccion.Estado;
            var nuevoEstado = Enum.Parse<EstadoInspeccion>(dto.NuevoEstado, ignoreCase: true);

            inspeccion.Estado = nuevoEstado;
            inspeccion.FechaActualizacion = DateTime.UtcNow;

            _inspeccionRepository.Update(inspeccion);
            var resultado = await _inspeccionRepository.SaveChangesAsync();

            if (resultado)
            {
                await _historialService.RegistrarEventoAsync(
                    id, usuarioId, "Estado modificado",
                    estadoAnterior.ToString(), nuevoEstado.ToString(), null);
            }

            return resultado;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var inspeccion = await _inspeccionRepository.GetByIdAsync(id);
            if (inspeccion is null) return false;

            _inspeccionRepository.Delete(inspeccion);
            return await _inspeccionRepository.SaveChangesAsync();
        }

        private static InspeccionDto MapToDto(Inspeccion i) => new()
        {
            Id = i.Id,
            UsuarioId = i.UsuarioId,
            NombreTecnico = i.Usuario?.Nombre ?? string.Empty,
            Titulo = i.Titulo,
            Descripcion = i.Descripcion,
            FechaInspeccion = i.FechaInspeccion,
            Latitud = i.Latitud,
            Longitud = i.Longitud,
            DireccionReferencia = i.DireccionReferencia,
            Estado = i.Estado.ToString(),
            FechaCreacion = i.FechaCreacion,
            FechaActualizacion = i.FechaActualizacion,
            Fotografias = i.Fotografias?.Select(f => new FotografiaDto
            {
                Id = f.Id,
                InspeccionId = f.InspeccionId,
                NombreArchivo = f.NombreArchivo,
                RutaArchivo = f.RutaArchivo,
                TipoContenido = f.TipoContenido,
                TamanoBytes = f.TamanoBytes,
                Descripcion = f.Descripcion,
                FechaRegistro = f.FechaRegistro
            }).ToList() ?? new(),
            Audios = i.Audios?.Select(a => new AudioDto
            {
                Id = a.Id,
                InspeccionId = a.InspeccionId,
                NombreArchivo = a.NombreArchivo,
                RutaArchivo = a.RutaArchivo,
                TipoContenido = a.TipoContenido,
                TamanoBytes = a.TamanoBytes,
                DuracionSegundos = a.DuracionSegundos,
                FechaRegistro = a.FechaRegistro
            }).ToList() ?? new(),
            Observaciones = i.Observaciones?.Select(o => new ObservacionDto
            {
                Id = o.Id,
                InspeccionId = o.InspeccionId,
                UsuarioId = o.UsuarioId,
                NombreUsuario = o.Usuario?.Nombre ?? string.Empty,
                Comentario = o.Comentario,
                FechaRegistro = o.FechaRegistro
            }).ToList() ?? new()
        };
    }
}