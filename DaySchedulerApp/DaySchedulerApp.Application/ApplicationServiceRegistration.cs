using DaySchedulerApp.Application.Contracts.Services;
using DaySchedulerApp.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DaySchedulerApp.Application
{
    public static class ApplicationServiceRegistration
    {


        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IAssignmentService, AssignmentService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();

            return services;
        }
    }
}