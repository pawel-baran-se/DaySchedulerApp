using DaySchedulerApp.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaySchedulerApp.Application.Contracts.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task<RegistrationResponse> Register(RegistrationRequest request);
        Task<bool> CreateRole(CreateRoleRequest request);
        Task<List<AppUser>> GetAplicationUsers();
    }
}
