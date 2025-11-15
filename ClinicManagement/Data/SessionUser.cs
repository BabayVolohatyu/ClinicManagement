
namespace ClinicManagement.Data
{
    public class SessionUser
    {
        public int Id { get; set; }
        public string Email { get; set; } = "";
        public string FirstName { get; set; } = "";
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
    }

}
