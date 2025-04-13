using IdentityService_API.models;

namespace IdentityService_API.Repository
{
    public interface IidentityUser
    {
        Task<Identity> GetByIdAsync(string id);
    }
}
