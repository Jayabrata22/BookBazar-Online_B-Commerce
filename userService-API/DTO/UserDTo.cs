﻿namespace userService_API.DTO
{
    public class UserDTO
    {
        public string Id { get; set; } 
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public long PhoneNumber { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
