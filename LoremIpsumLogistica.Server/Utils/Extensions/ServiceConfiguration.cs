using LoremIpsumLogistica.Application.Services;
using LoremIpsumLogistica.Server.Infraestructure.UnitOfWork;

namespace LoremIpsumLogistica.Server.Utils.Extensions
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<CustomerService>();

            return services;
        }
    }
}