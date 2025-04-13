using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using userService_API.DTO;
using userService_API.Service;

namespace userService_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController   (UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO dto)
        {
            await _userService.CreateUserAsync(dto);
            return Ok(new { message = "User created in UserService DB." });
        }
    }
}
