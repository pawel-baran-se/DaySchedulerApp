using DaySchedulerApp.Application.Contracts;
using DaySchedulerApp.Application.Contracts.Infrastructure;
using DaySchedulerApp.Application.Contracts.Services;
using DaySchedulerApp.Application.Models.Email;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DaySchedulerApp.Application.Services
{
    public class BackgroundWorkerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<BackgroundWorkerService> _logger;
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IEmailSender _emailSender;

        public BackgroundWorkerService(
            IServiceProvider serviceProvider,
            ILogger<BackgroundWorkerService> logger,
            IAssignmentRepository assignmentRepository,
            IEmailSender emailSender)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _assignmentRepository = assignmentRepository;
            _emailSender = emailSender;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker started at: {time}", DateTimeOffset.Now);

            while (true)
            {
                TimeOnly executingTime = new TimeOnly(13, 43);
                var now = DateTime.Now;
                if (now.Hour == executingTime.Hour && now.Minute == executingTime.Minute)
                {
                    var subject = $"Day Scheduler - Plan {now}";
                    using (IServiceScope scope = _serviceProvider.CreateScope())
                    {
                        var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
                        var users = await authService.GetAplicationUsers();
                        foreach (var user in users)
                        {
                            var assignments = await _assignmentRepository.GetNotifiableForUserById(user.Id);
                            string body = "Plan for today: ";
                            foreach (var assignment in assignments)
                            {
                                body += $"{assignment.Name} , ";
                            }

                            var email = new Email() { To = user.Email, Subject = subject, Body = body };

                            await _emailSender.SendEmail(email);
                        }
                    }
                }
                _logger.LogInformation("Worker round at: {time}", DateTimeOffset.Now);
                await Task.Delay(55000);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Background Worker stopped at {DateTime.Now}");
            await base.StopAsync(cancellationToken);
        }

    }
}