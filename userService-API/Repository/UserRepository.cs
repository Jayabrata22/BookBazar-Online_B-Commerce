using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using userService_API.DBContext;
using userService_API.Interfaces;
using userService_API.models;

namespace userService_API.Repository
{
    public class UserRepository : IUserRepository
    {
        private UserDBContext _context;

        public UserRepository(UserDBContext context)
        {
            _context = context;
        }
        public async Task AddUserAsync(UserProfile user)
        {
            _context.UserProfiles.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserProfile>> GetAllAsync()
        {
            return await _context.UserProfiles.ToListAsync();
        }

        public async Task<UserProfile> GetByIdAsync(string id)
        {
            var user = await _context.UserProfiles.FindAsync(id);
            if(user != null)
            {
                return user;
            }
            return null;
        }

        public Task UpdateUserdetailsAsync(UserProfile user)
        {
           var userProfile = _context.UserProfiles.FirstOrDefault(u => u.Id == user.Id);
            if (userProfile != null)
            {
                userProfile.FullName = user.FullName;
                userProfile.Email = user.Email;
                userProfile.PhoneNumber = user.PhoneNumber;
                userProfile.Address = user.Address;
                userProfile.ProfilePicture = user.ProfilePicture;
            }
            return _context.SaveChangesAsync();
        }

    }
}
