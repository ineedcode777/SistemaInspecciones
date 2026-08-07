using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SistemaInspecciones.Application.DTOs;
using SistemaInspecciones.Application.Interfaces.Repositories;
using SistemaInspecciones.Application.Interfaces.Services;
using SistemaInspecciones.Domain.Entities;

namespace SistemaInspecciones.Application.Services
{
    public class EvidenciaService : IEvidenciaService
    {
        private readonly IFotografiaRepository _fotografiaRepository;
        private readonly IAudioRepository _audioRepository;
        private readonly string _basePath;

        public EvidenciaService(IFotografiaRepository fotografiaRepository, IAudioRepository audioRepository, IConfiguration configuration)
        {
            _fotografiaRepository = fotografiaRepository;
            _audioRepository = audioRepository;
            _basePath = configuration["Storage:BasePath"] ?? Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            if (!Directory.Exists(_basePath))
                Directory.CreateDirectory(_basePath);
        }

        public async Task<FotografiaDto> AgregarFotografiaAsync(int inspeccionId, IFormFile archivo, string? descripcion)
        {
            var carpeta = Path.Combine(_basePath, "Fotografias");
            Directory.CreateDirectory(carpeta);

            var nombreArchivo = $"{Guid.NewGuid()}{Path.GetExtension(archivo.FileName)}";
            var rutaCompleta = Path.Combine(carpeta, nombreArchivo);

            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            var fotografia = new Fotografia
            {
                InspeccionId = inspeccionId,
                NombreArchivo = nombreArchivo,
                RutaArchivo = rutaCompleta,
                TipoContenido = archivo.ContentType,
                TamanoBytes = archivo.Length,
                Descripcion = descripcion,
                FechaRegistro = DateTime.UtcNow
            };

            await _fotografiaRepository.AddAsync(fotografia);
            await _fotografiaRepository.SaveChangesAsync();

            return MapFotoToDto(fotografia);
        }

        public async Task<IEnumerable<FotografiaDto>> GetFotografiasAsync(int inspeccionId)
        {
            var fotos = await _fotografiaRepository.GetByInspeccionIdAsync(inspeccionId);
            return fotos.Select(MapFotoToDto);
        }

        public async Task<bool> EliminarFotografiaAsync(int fotografiaId)
        {
            var foto = await _fotografiaRepository.GetByIdAsync(fotografiaId);
            if (foto is null) return false;

            if (File.Exists(foto.RutaArchivo))
                File.Delete(foto.RutaArchivo);

            _fotografiaRepository.Delete(foto);
            return await _fotografiaRepository.SaveChangesAsync();
        }

        public async Task<AudioDto> AgregarAudioAsync(int inspeccionId, IFormFile archivo, int? duracionSegundos)
        {
            var carpeta = Path.Combine(_basePath, "Audios");
            Directory.CreateDirectory(carpeta);

            var nombreArchivo = $"{Guid.NewGuid()}{Path.GetExtension(archivo.FileName)}";
            var rutaCompleta = Path.Combine(carpeta, nombreArchivo);

            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            var audio = new Audio
            {
                InspeccionId = inspeccionId,
                NombreArchivo = nombreArchivo,
                RutaArchivo = rutaCompleta,
                TipoContenido = archivo.ContentType,
                TamanoBytes = archivo.Length,
                DuracionSegundos = duracionSegundos,
                FechaRegistro = DateTime.UtcNow
            };

            await _audioRepository.AddAsync(audio);
            await _audioRepository.SaveChangesAsync();

            return MapAudioToDto(audio);
        }

        public async Task<IEnumerable<AudioDto>> GetAudiosAsync(int inspeccionId)
        {
            var audios = await _audioRepository.GetByInspeccionIdAsync(inspeccionId);
            return audios.Select(MapAudioToDto);
        }

        public async Task<bool> EliminarAudioAsync(int audioId)
        {
            var audio = await _audioRepository.GetByIdAsync(audioId);
            if (audio is null) return false;

            if (File.Exists(audio.RutaArchivo))
                File.Delete(audio.RutaArchivo);

            _audioRepository.Delete(audio);
            return await _audioRepository.SaveChangesAsync();
        }

        private static FotografiaDto MapFotoToDto(Fotografia f) => new()
        {
            Id = f.Id,
            InspeccionId = f.InspeccionId,
            NombreArchivo = f.NombreArchivo,
            RutaArchivo = f.RutaArchivo,
            TipoContenido = f.TipoContenido,
            TamanoBytes = f.TamanoBytes,
            Descripcion = f.Descripcion,
            FechaRegistro = f.FechaRegistro
        };

        private static AudioDto MapAudioToDto(Audio a) => new()
        {
            Id = a.Id,
            InspeccionId = a.InspeccionId,
            NombreArchivo = a.NombreArchivo,
            RutaArchivo = a.RutaArchivo,
            TipoContenido = a.TipoContenido,
            TamanoBytes = a.TamanoBytes,
            DuracionSegundos = a.DuracionSegundos,
            FechaRegistro = a.FechaRegistro
        };
    }
}