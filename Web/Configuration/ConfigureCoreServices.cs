using ApplicationCore.Entities;
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
            services.AddTransient(typeof(ITrainingPlanService), typeof(TrainingPlanService));
            services.AddTransient(typeof(IEmployeeService<>), typeof(EmployeeService<>));
            services.AddScoped<Func<Owner>>(sp => () => ActivatorUtilities.CreateInstance<Owner>(sp));
            services.AddScoped<Func<Manager>>(sp => () => ActivatorUtilities.CreateInstance<Manager>(sp));
            services.AddScoped<Func<PersonalTrainer>>(sp => () => ActivatorUtilities.CreateInstance<PersonalTrainer>(sp));
            services.AddScoped<Func<GroupTrainer>>(sp => () => ActivatorUtilities.CreateInstance<GroupTrainer>(sp));
            services.AddScoped<Func<Receptionist>>(sp => () => ActivatorUtilities.CreateInstance<Receptionist>(sp));
            services.AddTransient(typeof(IGymReportService), typeof(GymReportService));
            
            
            return services;
        }
    }
}
