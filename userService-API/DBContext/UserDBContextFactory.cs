using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using userService_API.DBContext; // Adjust this namespace to match your project

public class UserDBContextFactory : IDesignTimeDbContextFactory<UserDBContext>
{
    public UserDBContext CreateDbContext(string[] args)
    {
        string configPath = Path.Combine("C:\\Users\\dhyan\\source\\repos\\BookBazar\\userService-API");
        // Read config from appsettings.json
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(configPath) // EF Core looks here for config
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<UserDBContext>();
        var connectionString = configuration.GetConnectionString("BookBaazar_UserDb");

        optionsBuilder.UseSqlServer(connectionString);

        return new UserDBContext(optionsBuilder.Options);
    }
}
