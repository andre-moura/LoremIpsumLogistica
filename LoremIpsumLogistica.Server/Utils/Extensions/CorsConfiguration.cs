namespace LoremIpsumLogistica.Server.Utils.Extensions
{
    public static class CorsConfiguration
    {
        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors();

            return services;
        }
    }
}