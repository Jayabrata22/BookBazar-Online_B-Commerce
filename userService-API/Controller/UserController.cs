using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using userService_API.DTO;
using userService_API.models;
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

        [HttpGet]
        public async Task<IActionResult> getAllusers()
        {
            var allusers = await _userService.GetAlluser();
            if (allusers == null)
            {
                NoContent();
            }
            return Ok(allusers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserDTO dto)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();
            Console.WriteLine($"Current User: {user.FullName}, {user.Email}, {user.PhoneNumber}");
            Console.WriteLine($"DTO: {dto.FullName}, {dto.Email}, {dto.PhoneNumber}");
            // Start with a new UserProfile, using the current user's existing values
            var userProfile = new UserProfile
            {
                Id = user.Id,  // Always set to the current user's Id
                FullName = dto.FullName ?? user.FullName,  // If FullName is provided, update it, else keep the current one
                Email = dto.Email ?? user.Email,  // Same for Email
                PhoneNumber = dto.PhoneNumber != 0 ? dto.PhoneNumber : user.PhoneNumber,  // If PhoneNumber is 0, keep the existing one
                Address = dto.Address ?? user.Address,  // Same for Address
                ProfilePicture = dto.ProfilePicture ?? user.ProfilePicture  // Same for ProfilePicture
            };
            Console.WriteLine($"Updated Profile: {userProfile.FullName}, {userProfile.Email}, {userProfile.PhoneNumber}");
            // Update the user profile
            await _userService.UpdateUserAsync(userProfile);

            return Ok("Update Successfully");
        }

    }
}
