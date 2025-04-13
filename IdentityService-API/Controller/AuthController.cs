using IdentityService_API.DTO;
using IdentityService_API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var token = await _authService.RegisterAsync(dto);
            return token == null ? BadRequest("Registration failed") : Ok(new { token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var token = await _authService.LoginAsync(dto);
            return token == null ? Unauthorized() : Ok(new { token });
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            return Ok(new { message = "You have been logged out. Please discard your token." });
        }
    }
}
