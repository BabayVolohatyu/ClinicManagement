namespace ClinicManagement.Models.Auth
{
    public class PromotionRequest
    {
        public int Id { get; set; }

        
        public int UserId { get; set; }
        public User User { get; set; }

        
        public int RequestedRoleId { get; set; }
        public Role RequestedRole { get; set; } = Role.Guest;

        
        public string? Reason { get; set; }

        public DateTimeOffset RequestedAt { get; set; }

        public PromotionStatus Status { get; set; } = PromotionStatus.Pending;

        
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