using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaInspecciones.Api.Extensions;
using SistemaInspecciones.Application.DTOs;
using SistemaInspecciones.Application.Interfaces.Services;

namespace SistemaInspecciones.Api.Controllers
{
    [ApiController]
    [Route("api/inspecciones")]
    [Authorize]
    public class InspeccionesController : ControllerBase
    {
        private readonly IInspeccionService _inspeccionService;

        public InspeccionesController(IInspeccionService inspeccionService)
        {
            _inspeccionService = inspeccionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _inspeccionService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var inspeccion = await _inspeccionService.GetByIdAsync(id);
            return inspeccion is null ? NotFound() : Ok(inspeccion);
        }

        [HttpGet("mis-inspecciones")]
        public async Task<IActionResult> GetMisInspecciones()
        {
            var usuarioId = User.GetUsuarioId();
            return Ok(await _inspeccionService.GetByUsuarioAsync(usuarioId));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CrearInspeccionDto dto)
        {
            var usuarioId = User.GetUsuarioId();
            var inspeccion = await _inspeccionService.CreateAsync(usuarioId, dto);
            return CreatedAtAction(nameof(GetById), new { id = inspeccion.Id }, inspeccion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ActualizarInspeccionDto dto)
        {
            var resultado = await _inspeccionService.UpdateAsync(id, dto);
            return resultado ? NoContent() : BadRequest(new { mensaje = "No se pudo actualizar (no existe o está cerrada)." });
        }

        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> CambiarEstado(int id, [FromBody] ActualizarEstadoInspeccionDto dto)
        {
            var usuarioId = User.GetUsuarioId();
            var resultado = await _inspeccionService.CambiarEstadoAsync(id, usuarioId, dto);
            return resultado ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _inspeccionService.DeleteAsync(id);
            return resultado ? NoContent() : NotFound();
        }
    }
}