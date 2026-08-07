using SistemaInspecciones.Application.DTOs;
using SistemaInspecciones.Application.Interfaces.Repositories;
using SistemaInspecciones.Application.Interfaces.Services;
using SistemaInspecciones.Domain.Entities;
using SistemaInspecciones.Domain.Enums;

namespace SistemaInspecciones.Application.Services
{
    public class HistorialService : IHistorialService
    {
        private readonly IHistorialInspeccionRepository _historialRepository;

        public HistorialService(IHistorialInspeccionRepository historialRepository)
        {
            _historialRepository = historialRepository;
        }

        public async Task<IEnumerable<HistorialInspeccionDto>> GetByInspeccionIdAsync(int inspeccionId)
        {
            var historial = await _historialRepository.GetByInspeccionIdAsync(inspeccionId);
            return historial.Select(h => new HistorialInspeccionDto
            {
                Id = h.Id,
                InspeccionId = h.InspeccionId,
                NombreUsuario = h.Usuario?.Nombre ?? string.Empty,
                Accion = h.Accion,
                EstadoAnterior = h.EstadoAnterior?.ToString(),
                EstadoNuevo = h.EstadoNuevo?.ToString(),
                Detalle = h.Detalle,
                FechaRegistro = h.FechaRegistro
            });
        }

        public async Task RegistrarEventoAsync(int inspeccionId, int usuarioId, string accion, string? estadoAnterior, string? estadoNuevo, string? detalle)
        {
            var evento = new HistorialInspeccion
            {
                InspeccionId = inspeccionId,
                UsuarioId = usuarioId,
                Accion = accion,
                EstadoAnterior = estadoAnterior is null ? null : Enum.Parse<EstadoInspeccion>(estadoAnterior, true),
                EstadoNuevo = estadoNuevo is null ? null : Enum.Parse<EstadoInspeccion>(estadoNuevo, true),
                Detalle = detalle,
                FechaRegistro = DateTime.UtcNow
            };

            await _historialRepository.AddAsync(evento);
            await _historialRepository.SaveChangesAsync();
        }
    }
}