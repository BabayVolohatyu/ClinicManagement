namespace ClinicManagement.Models.Auth
{
    public class PasswordChangeRequest
    {
        public int Id { get; set; }

        // User who requested the password change
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        // Requested password hash (stored securely)
        public string RequestedPasswordHash { get; set; } = string.Empty;

        public DateTimeOffset RequestedAt { get; set; }

        public PasswordChangeStatus Status { get; set; } = PasswordChangeStatus.Pending;

        // Admin who processed the request
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

