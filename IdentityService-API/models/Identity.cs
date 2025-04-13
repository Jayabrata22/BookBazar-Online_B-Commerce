using Microsoft.AspNetCore.Identity;

namespace IdentityService_API.models
{
    public class Identity : IdentityUser
    {
        public string? FullName { get; set; }
        public DateTime CreatedAt { get; internal set; }
        public DateTime UpdatedAt { get; internal set; }
        public string Password { get; internal set; }
        public string Role { get; internal set; }
    }
}
