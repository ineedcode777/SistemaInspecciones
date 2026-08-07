using SistemaInspecciones.Application.DTOs;
using SistemaInspecciones.Application.Interfaces.Repositories;
using SistemaInspecciones.Application.Interfaces.Services;
using SistemaInspecciones.Domain.Entities;

namespace SistemaInspecciones.Application.Services
{
    public class ObservacionService : IObservacionService
    {
        private readonly IObservacionRepository _observacionRepository;
        private readonly IHistorialService _historialService;

        public ObservacionService(IObservacionRepository observacionRepository, IHistorialService historialService)
        {
            _observacionRepository = observacionRepository;
            _historialService = historialService;
        }

        public async Task<IEnumerable<ObservacionDto>> GetByInspeccionIdAsync(int inspeccionId)
        {
            var observaciones = await _observacionRepository.GetByInspeccionIdAsync(inspeccionId);
            return observaciones.Select(MapToDto);
        }

        public async Task<ObservacionDto> CreateAsync(int inspeccionId, int usuarioId, CrearObservacionDto dto)
        {
            var observacion = new Observacion
            {
                InspeccionId = inspeccionId,
                UsuarioId = usuarioId,
                Comentario = dto.Comentario,
                FechaRegistro = DateTime.UtcNow
            };

            await _observacionRepository.AddAsync(observacion);
            await _observacionRepository.SaveChangesAsync();

            await _historialService.RegistrarEventoAsync(inspeccionId, usuarioId, "Observacion agregada", null, null, dto.Comentario);

            return MapToDto(observacion);
        }

        public async Task<bool> UpdateAsync(int id, CrearObservacionDto dto)
        {
            var observacion = await _observacionRepository.GetByIdAsync(id);
            if (observacion is null) return false;

            observacion.Comentario = dto.Comentario;
            _observacionRepository.Update(observacion);
            return await _observacionRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var observacion = await _observacionRepository.GetByIdAsync(id);
            if (observacion is null) return false;

            _observacionRepository.Delete(observacion);
            return await _observacionRepository.SaveChangesAsync();
        }

        private static ObservacionDto MapToDto(Observacion o) => new()
        {
            Id = o.Id,
            InspeccionId = o.InspeccionId,
            UsuarioId = o.UsuarioId,
            NombreUsuario = o.Usuario?.Nombre ?? string.Empty,
            Comentario = o.Comentario,
            FechaRegistro = o.FechaRegistro
        };
    }
}