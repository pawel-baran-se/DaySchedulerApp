using DaySchedulerApp.Application.Contracts.Services;
using DaySchedulerApp.Application.Models.Identity;
using DaySchedulerApp.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DaySchedulerApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            return Ok(await _authService.Login(request));
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register([FromBody] RegistrationRequest request)
        {
            return Ok(await _authService.Register(request));
        }

        // POST api/<AccountController>
        [HttpPost]
        [Route("roles/add")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
        {
            await _authService.CreateRole(request);
            return Ok();
        }

        [HttpGet]
        [Route("users")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _authService.GetAplicationUsers());
        }


    }
}
