using DaySchedulerApp.Application.Contracts.Infrastructure;
using DaySchedulerApp.Application.Models.Email;
using DaySchedulerApp.Infrastructure.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaySchedulerApp.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {

        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSender"));
            services.AddTransient<IEmailSender, EmailSender>();

            return services;
        }
    }
}
