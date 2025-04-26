using userService_API.models;

namespace userService_API.Interfaces
{
    public interface IUserRepository
    {
        Task<UserProfile> GetByIdAsync(string id);
        Task<IEnumerable<UserProfile>> GetAllAsync();
        Task AddUserAsync(UserProfile user);
        Task UpdateUserdetailsAsync(UserProfile user);

    }
}
