using SistemaInspecciones.Mobile.Models;
using SistemaInspecciones.Mobile.Services;

namespace SistemaInspecciones.Mobile.Service
{
    public class AuthService
    {
        private readonly ApiService _apiService;
        private readonly TokenService _tokenService;

        public AuthService(ApiService apiService, TokenService tokenService)
        {
            _apiService = apiService;
            _tokenService = tokenService;
        }

        public async Task<(bool Success, string? Error)> LoginAsync(string correo, string password)
        {
            var request = new LoginRequest { Correo = correo, Password = password };
            var (success, data, error) = await _apiService.PostAsync<LoginResponse>("api/auth/login", request);

            if (!success || data is null)
                return (false, "Correo o contraseña incorrectos.");

            await _tokenService.GuardarSesionAsync(data.Token, data.UsuarioId, data.Nombre, data.Rol);
            return (true, null);
        }

        public async Task<bool> HaySesionActivaAsync() => await _tokenService.HaySesionActivaAsync();

        public void Logout() => _tokenService.CerrarSesion();
    }
}