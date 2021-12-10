using InfluxDb.Repositories;
using InfluxDb.Service;

namespace InfluxDb.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ResolveDependecias(this IServiceCollection services)
        {
            services.AddTransient<IVeiculoService, VeiculoService>();
            services.AddTransient<IInfluxDBRepository, InfluxDBRepository>();
            return services;
        }
    }
}
