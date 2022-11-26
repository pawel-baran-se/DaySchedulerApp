using DaySchedulerApp.Application.Contracts.Services;
using DaySchedulerApp.Application.Enum;
using DaySchedulerApp.Application.Models.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DaySchedulerApp.Application.Services
{

    public class AuthorizationService : IAuthorizationService
    {
        private const string _USERID = "uid";

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public AppUser GetCurrentUser()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(_USERID)?.Value;
            var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var userRole = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

            Enum.UserRole.TryParse<UserRole>(userRole, out UserRole role);

            AppUser appUser = new() { Id = userId, Name = userName, Email = userEmail, Role = role };

            return appUser;
        }

        public bool ValidateUserId(string assignmentUserId)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(_USERID)?.Value;
            if (userId == assignmentUserId)
                return true;

            return false;
        }
    }
}
