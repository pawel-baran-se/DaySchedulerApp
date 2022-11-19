using AspNetCore.Identity.MongoDbCore.Infrastructure;
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
        private readonly MongoDbIdentityConfiguration configuration;
        public IdentityConfiguration(IOptions<DayScheduleIdentitySettings> settings)
        {
            configuration = new MongoDbIdentityConfiguration
            {
                MongoDbSettings = new MongoDbSettings
                {
                    ConnectionString = settings.Value.ConnectionString,
                    DatabaseName = settings.Value.DatabaseName,
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

        public MongoDbIdentityConfiguration Configuration { get => configuration; }
    }
}
