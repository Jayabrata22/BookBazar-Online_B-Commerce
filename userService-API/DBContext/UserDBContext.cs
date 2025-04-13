using Microsoft.EntityFrameworkCore;
using userService_API.models;

namespace userService_API.DBContext
{
    public class UserDBContext : DbContext
    {
        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options) 
        {
        }
        //DbSets
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}
