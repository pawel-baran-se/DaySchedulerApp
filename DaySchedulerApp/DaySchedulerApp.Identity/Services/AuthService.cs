using DaySchedulerApp.Application.Contracts.Services;
using DaySchedulerApp.Application.Models.Identity;
using DaySchedulerApp.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DaySchedulerApp.Identity.Services
{
    public class AuthService : IAuthService
    {
        private const string _ROLE = "USER";

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IOptions<JwtSettings> _jwtSettings;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

        public AuthService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IOptions<JwtSettings> jwtSettings,
            IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings;
            _passwordHasher = passwordHasher;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new Exception($"User with {request.Email} not found.");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new Exception($"Credentials are incorrect.");
            }

            var token = await GenerateToken(user);

            var response = new LoginResponse()
            {
                Id = user.Id.ToString(),
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Email = user.Email,
                UserName = user.UserName
            };

            return response;
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.UserName);

            if (existingUser != null)
            {
                throw new Exception($"Username {request.UserName} already exists.");
            }

            var user = new ApplicationUser()
            {
                Email = request.Email,
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
            };

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, _ROLE);
                    return new RegistrationResponse() { UserId = user.Id.ToString() };
                }
                else
                {
                    throw new Exception($"{result.Errors}");
                }
            }
            else
            {
                throw new Exception($"Email {request.Email} already exists.");
            }


        }


        public async Task<bool> CreateRole(CreateRoleRequest request)
        {
            var appRole = new ApplicationRole { Name = request.RoleName };
            var createRole = await _roleManager.CreateAsync(appRole);

            return true;
        }



        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x));

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id.ToString())
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.Key));
            var signingCridentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Value.Issuer,
                audience: _jwtSettings.Value.Audience,
                claims: claims,
                signingCredentials: signingCridentials,
                expires: DateTime.Now.AddMinutes(_jwtSettings.Value.DurationInMinutes)
                );

            return jwtSecurityToken;
        }



    }
}
