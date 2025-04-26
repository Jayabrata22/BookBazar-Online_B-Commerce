using IdentityService_API.DTO;
using IdentityService_API.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace IdentityService_API.Service
{
    public class AuthService
    {
        private readonly UserManager<Identity> _userManager;
        private readonly SignInManager<Identity> _signInManager;
        private readonly IConfiguration _config;
        private readonly HttpClient httpClient;

        public AuthService(UserManager<Identity> userManager, SignInManager<Identity> signInManager, IConfiguration config , HttpClient httpClient)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            this.httpClient = httpClient;
        }

        public string GenerateJwtToken(Identity user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string?> RegisterAsync(RegisterDTO dto)
        {
            var user = new Identity { UserName = dto.Username, FullName = dto.FullName };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return ("false");
            
            var userProfile = new
            {
                id = user.Id,
                email = user.NormalizedUserName,
                fullName = dto.FullName
            };
            var response = await httpClient.PostAsJsonAsync(" https://localhost:7232/api/user", userProfile);
            return result.Succeeded ? GenerateJwtToken(user) : null;
        }

        public async Task<string> LoginAsync(LoginDTO dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Username);
            if (user == null || !(await _userManager.CheckPasswordAsync(user, dto.Password))) return null;
            return GenerateJwtToken(user);
        }
    }
}
