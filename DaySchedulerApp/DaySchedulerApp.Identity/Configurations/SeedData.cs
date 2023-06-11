using DaySchedulerApp.Application.Models.Email;
using DaySchedulerApp.Application.Models.Identity;
using DaySchedulerApp.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Configuration;

namespace DaySchedulerApp.Identity.Configurations
{
	public class SeedData
	{
		private const string _USEROLE = "USER";
		private const string _ADMINROLE = "ADMINISTRATOR";

		private readonly RoleManager<ApplicationRole> _roleManager;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IConfiguration _configuration;
		private readonly IServiceCollection _services;
		private readonly ServiceProvider _serviceProvider;

		public SeedData(IConfiguration configuration, IServiceCollection services)
		{
			_configuration = configuration;
			_services = services;
			_serviceProvider = _services.BuildServiceProvider();
			_roleManager = _serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
			_userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
		}

		public async Task Seed()
		{

			if (!await _roleManager.RoleExistsAsync(_ADMINROLE))
			{
				var appRole = new ApplicationRole { Name = _ADMINROLE };
				await _roleManager.CreateAsync(appRole);
			}

			if (!await _roleManager.RoleExistsAsync(_USEROLE))
			{
				var appRole = new ApplicationRole { Name = _USEROLE };
				await _roleManager.CreateAsync(appRole);
			}

			var adminUser = await _userManager.FindByNameAsync("admin");

			if(adminUser == null)
			{
				var adminSettings = _configuration.GetSection("AdminSettings").Get<AdminSettings>();

				adminUser = new ApplicationUser
				{
					UserName = adminSettings.UserName,
					FirstName = "Pawel",
					LastName = "Baran",
					Email = adminSettings.Email
				};

				await _userManager.CreateAsync(adminUser, adminSettings.Password);

				await _userManager.AddToRoleAsync(adminUser, _ADMINROLE);
			}
		}
	}
}