
using ApplicationCore.Interfaces;
using ApplicationCore.Services;

namespace Web.Configuration
{
    public static class ConfigureCoreServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped(typeof(IClientService), typeof(ClientService));
            
            return services;
        }
    }
}
