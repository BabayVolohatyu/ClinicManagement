namespace ClinicManagement.Models.Auth
{
    public class PromotionRequest
    {
        public int Id { get; set; }

        // User who requested the promotion
        public int UserId { get; set; }
        public User User { get; set; }

        // Role that the user wants to be promoted to
        public int RequestedRoleId { get; set; }
        public Role RequestedRole { get; set; }

        // Reason for the promotion request
        public string? Reason { get; set; }

        public DateTimeOffset RequestedAt { get; set; }

        public PromotionStatus Status { get; set; } = PromotionStatus.Pending;

        // Admin who processed the request
        public int? ProcessedByAdminId { get; set; }
        public User? ProcessedByAdmin { get; set; }

        public DateTimeOffset? ProcessedAt { get; set; }
    }

    public enum PromotionStatus
    {
        Pending,
        Approved,
        Rejected
    }
}