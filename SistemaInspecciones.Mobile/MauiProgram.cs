using Microsoft.Extensions.Logging;
using SistemaInspecciones.Mobile.Service;
using SistemaInspecciones.Mobile.Services;
using SistemaInspecciones.Mobile.ViewModels;


namespace SistemaInspecciones.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<TokenService>();
            builder.Services.AddSingleton<ApiService>();
            builder.Services.AddSingleton<AuthService>();

            builder.Services.AddTransient<LoginViewModel>();
            return builder.Build();
        }
    }
}
