using ClinicManagement.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Data
{
    public static class AuthDataSeeder
    {
        public static void SeedAuthData(this ModelBuilder modelBuilder)
        {
            // Seed Roles
            var roles = new[]
            {
                new Role
                {
                    Id = 1,
                    Name = "Guest",
                    CanRead = true, // Guests can only view data
                    CanAskPromotion = true // Guests can ask to become authorized users
                    // All other permissions are false by default
                },
                new Role
                {
                    Id = 2,
                    Name = "Authorized",
                    CanRead = true,
                    CanDownloadCsv = true,
                    CanAskPromotion = true
                    // Can view tables and download as CSV, ask for promotion
                },
                new Role
                {
                    Id = 3,
                    Name = "Operator",
                    CanCreate = true,
                    CanRead = true,
                    CanUpdate = true,
                    CanDelete = true,
                    CanExecuteRawQueries = true,
                    CanDownloadCsv = true
                    // Full database operations but cannot manage promotions
                },
                new Role
                {
                    Id = 4,
                    Name = "Admin",
                    CanCreate = true,
                    CanRead = true,
                    CanUpdate = true,
                    CanDelete = true,
                    CanExecuteRawQueries = true,
                    CanAcceptPromotions = true,
                    CanViewPromotionsList = true,
                    CanManageUsers = true,
                    CanViewUserData = true,
                    CanDownloadCsv = true
                    // Full system access including user management
                }
            };

            modelBuilder.Entity<Role>().HasData(roles);
        }
    }
}