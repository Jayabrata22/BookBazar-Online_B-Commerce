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

        public Task<IEnumerable<UserProfile>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserProfile> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
