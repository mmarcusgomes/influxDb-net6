namespace InfluxDb.Configuration
{
    public static class AppSettingsExtensions
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("InfluxDb"); // Melhorar isso
            services.Configure<InfluxDbOptions>(appSettingsSection);          
            return services;
        }
    }
}
