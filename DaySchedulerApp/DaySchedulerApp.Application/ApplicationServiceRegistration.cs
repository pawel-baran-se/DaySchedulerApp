﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DaySchedulerApp.Application
{
    public static class ApplicationServiceRegistration
    {


        public static IServiceCollection DaySchedulerApplicationRegistration(this IServiceCollection services)
        {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}