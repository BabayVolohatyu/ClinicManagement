namespace ClinicManagement.Models.Auth
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public Role? Role { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        
        public IEnumerable<PromotionRequest> PromotionRequests { get; set; } = new List<PromotionRequest>();
    }
}
