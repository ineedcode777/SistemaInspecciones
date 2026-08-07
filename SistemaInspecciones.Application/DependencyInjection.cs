using Microsoft.Extensions.DependencyInjection;
using SistemaInspecciones.Application.Interfaces.Services;
using SistemaInspecciones.Application.Services;

namespace SistemaInspecciones.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IInspeccionService, InspeccionService>();
            services.AddScoped<IObservacionService, ObservacionService>();
            services.AddScoped<IHistorialService, HistorialService>();
            services.AddScoped<IEvidenciaService, EvidenciaService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            return services;
        }
    }
}