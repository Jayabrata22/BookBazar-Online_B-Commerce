using IdentityService_API.models;

namespace IdentityService_API.Repository
{
    public class IidentityuserRepository : IidentityUser
    {
        public Task<Identity> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
