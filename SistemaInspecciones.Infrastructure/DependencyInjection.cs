using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaInspecciones.Application.Interfaces.Repositories;
using SistemaInspecciones.Infrastructure.Data;
using SistemaInspecciones.Infrastructure.Repositories;

namespace SistemaInspecciones.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IInspeccionRepository, InspeccionRepository>();
            services.AddScoped<IFotografiaRepository, FotografiaRepository>();
            services.AddScoped<IAudioRepository, AudioRepository>();
            services.AddScoped<IObservacionRepository, ObservacionRepository>();
            services.AddScoped<IHistorialInspeccionRepository, HistorialInspeccionRepository>();

            return services;
        }
    }
}