using DaySchedulerApp.Application.Contracts;
using DaySchedulerApp.Application.Contracts.Infrastructure;
using DaySchedulerApp.Application.Contracts.Services;
using DaySchedulerApp.Application.Models.Email;
using DaySchedulerApp.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text;

namespace DaySchedulerApp.Application.Services
{
    public class BackgroundWorkerService : BackgroundService
    {
        private const int _HOUR = 14;
        private const int _MINUTE = 50;


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
                await SendAssignmentList();

                _logger.LogInformation("Working: {time}", DateTimeOffset.Now);
                await Task.Delay(55000);
            }
        }

        private async Task SendAssignmentList()
        {
            TimeOnly executingTime = new TimeOnly(_HOUR, _MINUTE);
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
                        var assignments = await _assignmentRepository.GetCurrentAssignmentsForUser(user.Id);
                        if (assignments != null)
                        {
                            string body = GenerateAssignmentList(assignments);

                            var email = new Email() { To = user.Email, Subject = subject, Body = body };

                            await _emailSender.SendEmail(email);
                        }
                    }
                }
            }
        }

        private string GenerateAssignmentList(List<Assignment> assignments)
        {
            StringBuilder sb = new();

            string body = "<h2>Plan for today: </h2>";

            sb.AppendLine(body);
            sb.AppendLine("<ul>");
            var counter = 1;
            foreach (var assignment in assignments)
            {
                var assignmentPosition = $"<li>{counter}: {assignment.Name}</li>";
                sb.AppendLine(assignmentPosition);
                counter ++;
            }
            sb.AppendLine("</ul>");

            return sb.ToString();
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Background Worker stopped at {DateTime.Now}");
            await base.StopAsync(cancellationToken);
        }

    }
}