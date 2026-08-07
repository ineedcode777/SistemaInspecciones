using Microsoft.AspNetCore.Http;
using SistemaInspecciones.Application.DTOs;

namespace SistemaInspecciones.Application.Interfaces.Services
{
    public interface IEvidenciaService
    {
        Task<FotografiaDto> AgregarFotografiaAsync(int inspeccionId, IFormFile archivo, string? descripcion);
        Task<IEnumerable<FotografiaDto>> GetFotografiasAsync(int inspeccionId);
        Task<bool> EliminarFotografiaAsync(int fotografiaId);

        Task<AudioDto> AgregarAudioAsync(int inspeccionId, IFormFile archivo, int? duracionSegundos);
        Task<IEnumerable<AudioDto>> GetAudiosAsync(int inspeccionId);
        Task<bool> EliminarAudioAsync(int audioId);
    }
}