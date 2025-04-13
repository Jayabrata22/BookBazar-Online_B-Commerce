using IdentityService_API.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace IdentityService_API.DBContext
{
    public class IdentityServiceDBContext : IdentityDbContext
    {
        public IdentityServiceDBContext(DbContextOptions<IdentityServiceDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.SeedRoles();
            builder.SeedSuperAdmin();
        }
    }
    public static class ModelBuilderExtensions
    {
        public static void SeedRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "role-admin-id",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "role-seller-id",
                    Name = "Seller",
                    NormalizedName = "SELLER"
                },
                new IdentityRole
                {
                    Id = "role-customer-id",
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                }
            );
        }

        public static void SeedSuperAdmin(this ModelBuilder modelBuilder)
        {
           
            DateTime seedDate = new DateTime (2025, 4, 5, 0, 0, 0);
            var adminUser = new Identity
            {
                Id = "admin-user-id",
                UserName = "admin@clothshop.com",
                NormalizedUserName = "ADMIN@CLOTHSHOP.COM",
                Email = "admin@clothshop.com",
                NormalizedEmail = "ADMIN@CLOTHSHOP.COM",
                EmailConfirmed = true,
                PhoneNumber = "8250333465",
                PhoneNumberConfirmed = true,
                FullName = "Super Admin",
                CreatedAt = seedDate,
                UpdatedAt = seedDate,
                SecurityStamp = "stamp-admin-1234",               
                Password = "Admin@123",
                Role = "Admin",
                ConcurrencyStamp = "concurrency-admin-1234",
                PasswordHash = "AQAAAAEAACcQAAAAEGp0YX0QWhHi19pK9xQ1E9y+E..." // Static
            };

            var sellerUser = new Identity
            {
                Id = "seller-user-id",
                UserName = "seller@clothshop.com",
                NormalizedUserName = "SELLER@CLOTHSHOP.COM",
                Email = "seller@clothshop.com",
                NormalizedEmail = "SELLER@CLOTHSHOP.COM",
                EmailConfirmed = true,
                PhoneNumber = "8597628237",
                PhoneNumberConfirmed = true,
                FullName = "Default Seller",
                Password = "Seller@123",
                CreatedAt = seedDate,
                UpdatedAt = seedDate,              
                Role = "Seller",
                SecurityStamp = "stamp-seller-5678",
                ConcurrencyStamp = "concurrency-seller-5678",
                PasswordHash = "AQAAAAEAACcQAAAAEGf4hTnUmN3FhM79pkLTTxXey..." // Static
            };

            var customerUser = new Identity
            {
                Id = "customer-user-id",
                UserName = "customer@clothshop.com",
                NormalizedUserName = "CUSTOMER@CLOTHSHOP.COM",
                Email = "customer@clothshop.com",
                NormalizedEmail = "CUSTOMER@CLOTHSHOP.COM",
                EmailConfirmed = true,
                PhoneNumber = "7777777777",
                PhoneNumberConfirmed = true,
                FullName = "Default Customer",
                Password = "Customer@123",
                CreatedAt = seedDate,
                UpdatedAt = seedDate,               
                Role = "Customer",
                SecurityStamp = "stamp-customer-9012",
                ConcurrencyStamp = "concurrency-customer-9012",
                PasswordHash = "AQAAAAEAACcQAAAAEKkkJLpddw0k+1qJ+t9mAxVfA..." // Static
            };

            modelBuilder.Entity<Identity>().HasData(adminUser, sellerUser, customerUser);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = "role-admin-id", UserId = "admin-user-id" },
                new IdentityUserRole<string> { RoleId = "role-seller-id", UserId = "admin-user-id" },
                new IdentityUserRole<string> { RoleId = "role-customer-id", UserId = "admin-user-id" },

                new IdentityUserRole<string> { RoleId = "role-seller-id", UserId = "seller-user-id" },
                new IdentityUserRole<string> { RoleId = "role-customer-id", UserId = "customer-user-id" }
            );
        }

    }
}
