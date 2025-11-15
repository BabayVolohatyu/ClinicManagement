using ClinicManagement.Data;
using ClinicManagement.Models.Auth;

namespace ClinicManagement.Services
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(string email, string password);
        Task<bool> RegisterAsync(RegisterModel model);
        void LoginUser(User user);
        void Logout();
        SessionUser GetCurrentUser();
        bool IsAuthenticated();
        bool IsGuest();
        void SetGuest(); 
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> UserExistsAsync(string email);
        Task<Role> GetRoleByNameAsync(string roleName);
    }
}