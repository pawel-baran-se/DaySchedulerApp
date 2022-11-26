using DaySchedulerApp.Application.Enum;
using DaySchedulerApp.Application.Models.Identity;

namespace DaySchedulerApp.Application.Contracts.Services
{
    public interface IAuthorizationService
    {
        AppUser GetCurrentUser();

        bool ValidateUserId(string assignmentUserId);
    }
}