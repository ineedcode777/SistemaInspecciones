using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaInspecciones.Application.Interfaces.Services;

namespace SistemaInspecciones.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class HistorialController : ControllerBase
    {
        private readonly IHistorialService _historialService;

        public HistorialController(IHistorialService historialService)
        {
            _historialService = historialService;
        }

        [HttpGet("api/inspecciones/{inspeccionId}/historial")]
        public async Task<IActionResult> GetByInspeccion(int inspeccionId) =>
            Ok(await _historialService.GetByInspeccionIdAsync(inspeccionId));
    }
}