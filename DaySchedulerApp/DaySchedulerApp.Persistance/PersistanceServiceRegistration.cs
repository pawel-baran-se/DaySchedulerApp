using DaySchedulerApp.Persistance.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaySchedulerApp.Persistance
{
    public static class PersistanceServiceRegistration
    {
        public static IServiceCollection RepositoryRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DaySchedulerDatabaseSettings>(configuration.GetSection("DaySchedulerDatabase"));

            return services;
        }
    }
}
