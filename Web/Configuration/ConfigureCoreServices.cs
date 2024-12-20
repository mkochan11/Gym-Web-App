﻿
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
            services.AddScoped(typeof(IClientService), typeof(ClientService));
            services.AddScoped(typeof(IMembershipService), typeof(MembershipService));

            return services;
        }
    }
}
