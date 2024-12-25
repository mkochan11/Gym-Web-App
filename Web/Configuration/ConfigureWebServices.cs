﻿

using Web.Interfaces;
using Web.Services;

namespace Web.Configuration
{
    public static class ConfigureWebServices
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddTransient(typeof(IMembershipViewModelService), typeof(MembershipViewModelService));
            services.AddTransient(typeof(IMembershipBuyViewModelService), typeof(MembershipBuyViewModelService));
            services.AddTransient(typeof(ITrainingsCalendarViewModelService), typeof(TrainingsCalendarViewModelService));
            return services;
        }
    }
}
