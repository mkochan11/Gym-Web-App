

namespace Web.Configuration
{
    public static class ConfigureWebServices
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration) 
        {
            //services.AddScoped<IFleetViewModelService, FleetViewModelService>();
            
            return services;
        }
    }
}
