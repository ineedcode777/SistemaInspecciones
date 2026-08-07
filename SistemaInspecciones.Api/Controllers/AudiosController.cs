using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaInspecciones.Application.Interfaces.Services;

namespace SistemaInspecciones.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class AudiosController : ControllerBase
    {
        private readonly IEvidenciaService _evidenciaService;

        public AudiosController(IEvidenciaService evidenciaService)
        {
            _evidenciaService = evidenciaService;
        }

        [HttpGet("api/inspecciones/{inspeccionId}/audios")]
        public async Task<IActionResult> GetByInspeccion(int inspeccionId) =>
            Ok(await _evidenciaService.GetAudiosAsync(inspeccionId));

        [HttpPost("api/inspecciones/{inspeccionId}/audios")]
        public async Task<IActionResult> Upload(int inspeccionId, IFormFile archivo, [FromForm] int? duracionSegundos)
        {
            if (archivo is null || archivo.Length == 0)
                return BadRequest(new { mensaje = "Debe adjuntar un archivo." });

            var resultado = await _evidenciaService.AgregarAudioAsync(inspeccionId, archivo, duracionSegundos);
            return Ok(resultado);
        }

        [HttpDelete("api/audios/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _evidenciaService.EliminarAudioAsync(id);
            return resultado ? NoContent() : NotFound();
        }
    }
}