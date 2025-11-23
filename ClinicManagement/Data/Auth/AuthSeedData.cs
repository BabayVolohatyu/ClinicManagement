using ClinicManagement.Models.Auth;
using System.Security.Cryptography;
using System.Text;

namespace ClinicManagement.Data.Auth
{
    public static class AuthSeedData
    {
        public static List<User> GetSeedData()
        {
            return new List<User>
            {
                new User
                {
                    Id = 1,
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@clinic.local",
                    PasswordHash = HashPassword("0000"),
                    RoleId = 4, // Admin
                    CreatedAt = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero)
                },
                new User
                {
                    Id = 2,
                    FirstName = "Authorized",
                    LastName = "User",
                    Email = "authorized@clinic.local",
                    PasswordHash = HashPassword("0000"),
                    RoleId = 2, // Authorized
                    CreatedAt = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero)
                },
                new User
                {
                    Id = 3,
                    FirstName = "Operator",
                    LastName = "User",
                    Email = "operator@clinic.local",
                    PasswordHash = HashPassword("0000"),
                    RoleId = 3, // Operator
                    CreatedAt = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero)
                }
            };
        }

        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}

