using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaInspecciones.Api.Extensions;
using SistemaInspecciones.Application.DTOs;
using SistemaInspecciones.Application.Interfaces.Services;

namespace SistemaInspecciones.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class ObservacionesController : ControllerBase
    {
        private readonly IObservacionService _observacionService;

        public ObservacionesController(IObservacionService observacionService)
        {
            _observacionService = observacionService;
        }

        [HttpGet("api/inspecciones/{inspeccionId}/observaciones")]
        public async Task<IActionResult> GetByInspeccion(int inspeccionId) =>
            Ok(await _observacionService.GetByInspeccionIdAsync(inspeccionId));

        [HttpPost("api/inspecciones/{inspeccionId}/observaciones")]
        public async Task<IActionResult> Create(int inspeccionId, [FromBody] CrearObservacionDto dto)
        {
            var usuarioId = User.GetUsuarioId();
            var resultado = await _observacionService.CreateAsync(inspeccionId, usuarioId, dto);
            return Ok(resultado);
        }

        [HttpPut("api/observaciones/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CrearObservacionDto dto)
        {
            var resultado = await _observacionService.UpdateAsync(id, dto);
            return resultado ? NoContent() : NotFound();
        }

        [HttpDelete("api/observaciones/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _observacionService.DeleteAsync(id);
            return resultado ? NoContent() : NotFound();
        }
    }
}