namespace SistemaInspecciones.Mobile.Services
{
    public class TokenService
    {
        private const string TokenKey = "auth_token";
        private const string UsuarioIdKey = "usuario_id";
        private const string NombreKey = "usuario_nombre";
        private const string RolKey = "usuario_rol";

        public async Task GuardarSesionAsync(string token, int usuarioId, string nombre, string rol)
        {
            await SecureStorage.Default.SetAsync(TokenKey, token);
            await SecureStorage.Default.SetAsync(UsuarioIdKey, usuarioId.ToString());
            await SecureStorage.Default.SetAsync(NombreKey, nombre);
            await SecureStorage.Default.SetAsync(RolKey, rol);
        }

        public async Task<string?> ObtenerTokenAsync() => await SecureStorage.Default.GetAsync(TokenKey);

        public async Task<int> ObtenerUsuarioIdAsync()
        {
            var valor = await SecureStorage.Default.GetAsync(UsuarioIdKey);
            return int.TryParse(valor, out var id) ? id : 0;
        }

        public async Task<string?> ObtenerNombreAsync() => await SecureStorage.Default.GetAsync(NombreKey);

        public async Task<string?> ObtenerRolAsync() => await SecureStorage.Default.GetAsync(RolKey);

        public async Task<bool> HaySesionActivaAsync()
        {
            var token = await ObtenerTokenAsync();
            return !string.IsNullOrEmpty(token);
        }

        public void CerrarSesion() => SecureStorage.Default.RemoveAll();
    }
}