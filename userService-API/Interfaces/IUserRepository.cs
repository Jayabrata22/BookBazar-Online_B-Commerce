using userService_API.models;

namespace userService_API.Interfaces
{
    public interface IUserRepository
    {
        Task<UserProfile> GetByIdAsync(int id);
        Task<IEnumerable<UserProfile>> GetAllAsync();
        Task AddUserAsync(UserProfile user);

    }
}
