namespace ClinicManagement.Validators.Auth
{
    public class PromotionRequestModelValidator : ModelValidator
    {
        protected override Dictionary<string, string> ForeignKeyErrorMappings => new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["UserId"] = "User is required",
            ["RequestedRoleId"] = "Requested Role is required"
        };

        protected override Dictionary<string, string> DisplayNameMappings => new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["UserId"] = "User",
            ["RequestedRoleId"] = "Requested Role",
            ["Reason"] = "Reason",
            ["RequestedAt"] = "Requested At",
            ["Status"] = "Status",
            ["ProcessedByAdminId"] = "Processed By Admin",
            ["ProcessedAt"] = "Processed At"
        };

        protected override string[] NavigationPropertyIndicators => new[]
        {
            "User",
            "RequestedRole",
            "ProcessedByAdmin"
        };
    }
}

