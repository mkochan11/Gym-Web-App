
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data;

namespace Web.Configuration
{
    public static class ConfigureCoreServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddTransient(typeof(IClientService), typeof(ClientService));
            services.AddTransient(typeof(IMembershipService), typeof(MembershipService));
            services.AddTransient(typeof(IMembershipPlanService), typeof(MembershipPlanService));
            services.AddTransient(typeof(IGroupTrainingService), typeof(GroupTrainingService));
            services.AddTransient(typeof(IIndividualTrainingService), typeof(IndividualTrainingService));

            return services;
        }
    }
}
