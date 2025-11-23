namespace ClinicManagement.Validators.Auth
{
    public class PasswordChangeRequestModelValidator : ModelValidator
    {
        protected override Dictionary<string, string> ForeignKeyErrorMappings => new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["UserId"] = "User is required"
        };

        protected override Dictionary<string, string> DisplayNameMappings => new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["UserId"] = "User",
            ["Status"] = "Status",
            ["RequestedAt"] = "Requested At",
            ["ProcessedByAdminId"] = "Processed By Admin",
            ["ProcessedAt"] = "Processed At"
        };

        protected override string[] NavigationPropertyIndicators => new[]
        {
            "User",
            "ProcessedByAdmin"
        };
    }
}

