using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using SistemaInspecciones.Mobile.Service;


namespace SistemaInspecciones.Mobile.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly AuthService _authService;

        [ObservableProperty]
        private string correo = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        public LoginViewModel(AuthService authService)
        {
            _authService = authService;
            Title = "Iniciar sesión";
        }

        [RelayCommand]
        private async Task IniciarSesionAsync()
        {
            if (IsBusy) return;

            MensajeError = string.Empty;

            if (string.IsNullOrWhiteSpace(Correo) || string.IsNullOrWhiteSpace(Password))
            {
                MensajeError = "Debes ingresar correo y contraseña.";
                return;
            }

            try
            {
                IsBusy = true;
                var (success, error) = await _authService.LoginAsync(Correo, Password);

                if (!success)
                {
                    MensajeError = error ?? "No se pudo iniciar sesión.";
                    return;
                }

                // Navegación a la página principal tras login exitoso
                await Shell.Current.GoToAsync("//InspeccionesPage");
            }
            catch (Exception ex)
            {
                MensajeError = $"Error de conexión: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}