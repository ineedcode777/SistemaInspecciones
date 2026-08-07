using Microsoft.AspNetCore.Mvc;
using SistemaInspecciones.Application.DTOs;
using SistemaInspecciones.Application.Interfaces.Services;

namespace SistemaInspecciones.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUsuarioService _usuarioService;

        public AuthController(IAuthService authService, IUsuarioService usuarioService)
        {
            _authService = authService;
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var resultado = await _authService.LoginAsync(request);

            if (resultado is null)
                return Unauthorized(new { mensaje = "Correo o contraseña incorrectos, o usuario inactivo." });

            return Ok(resultado);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CrearUsuarioDto dto)
        {
            var usuario = await _usuarioService.CreateAsync(dto);
            return CreatedAtAction(nameof(Login), new { }, usuario);
        }
    }
}