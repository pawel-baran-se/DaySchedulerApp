using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaySchedulerApp.Identity.Configurations
{
    public class IdentityConfiguration
    {
        private readonly MongoDbIdentityConfiguration identityConfiguration;

        public IdentityConfiguration(IConfiguration configuration)
        {
            var settings = configuration.GetSection("DaySchedulerDatabase").Get<DayScheduleIdentitySettings>();
            identityConfiguration = new MongoDbIdentityConfiguration
            {

                MongoDbSettings = new MongoDbSettings
                {
                    ConnectionString = settings.ConnectionString,
                    DatabaseName = settings.DatabaseName,
                },
                IdentityOptionsAction = options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireLowercase = false;

                    //lockout
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 5;

                    options.User.RequireUniqueEmail = true;
                }
            };
        }

        public MongoDbIdentityConfiguration Configuration { get => identityConfiguration; }
    }
}
