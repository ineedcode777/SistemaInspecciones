using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaInspecciones.Application.Interfaces.Services;

namespace SistemaInspecciones.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class FotografiasController : ControllerBase
    {
        private readonly IEvidenciaService _evidenciaService;

        public FotografiasController(IEvidenciaService evidenciaService)
        {
            _evidenciaService = evidenciaService;
        }

        [HttpGet("api/inspecciones/{inspeccionId}/fotografias")]
        public async Task<IActionResult> GetByInspeccion(int inspeccionId) =>
            Ok(await _evidenciaService.GetFotografiasAsync(inspeccionId));

        [HttpPost("api/inspecciones/{inspeccionId}/fotografias")]
        public async Task<IActionResult> Upload(int inspeccionId, IFormFile archivo, [FromForm] string? descripcion)
        {
            if (archivo is null || archivo.Length == 0)
                return BadRequest(new { mensaje = "Debe adjuntar un archivo." });

            var resultado = await _evidenciaService.AgregarFotografiaAsync(inspeccionId, archivo, descripcion);
            return Ok(resultado);
        }

        [HttpDelete("api/fotografias/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _evidenciaService.EliminarFotografiaAsync(id);
            return resultado ? NoContent() : NotFound();
        }
    }
}