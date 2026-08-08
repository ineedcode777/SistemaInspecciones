using CommunityToolkit.Mvvm.ComponentModel;


namespace SistemaInspecciones.Mobile.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string title = string.Empty;

        [ObservableProperty]
        private string mensajeError = string.Empty;

        public bool IsNotBusy => !IsBusy;
    }
}