namespace ClinicManagement.Models.Auth
{
    public class PasswordChangeRequest
    {
        public int Id { get; set; }

        
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        
        public string RequestedPasswordHash { get; set; } = string.Empty;

        public DateTimeOffset RequestedAt { get; set; }

        public PasswordChangeStatus Status { get; set; } = PasswordChangeStatus.Pending;

        
        public int? ProcessedByAdminId { get; set; }
        public User? ProcessedByAdmin { get; set; }

        public DateTimeOffset? ProcessedAt { get; set; }
    }

    public enum PasswordChangeStatus
    {
        Pending,
        Approved,
        Rejected
    }
}

