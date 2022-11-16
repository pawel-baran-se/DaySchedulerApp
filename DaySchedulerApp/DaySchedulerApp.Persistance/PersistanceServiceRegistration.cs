using DaySchedulerApp.Application.Contracts;
using DaySchedulerApp.Persistance.Configurations;
using DaySchedulerApp.Persistance.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DaySchedulerApp.Persistance
{
    public static class PersistanceServiceRegistration
    {
        public static IServiceCollection ConfigureRepositoryServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DaySchedulerDatabaseSettings>(configuration.GetSection("DaySchedulerDatabase"));

            services.AddSingleton<IAssignmentRepository, AssignmentRepository>();


            return services;
        }
    }
}