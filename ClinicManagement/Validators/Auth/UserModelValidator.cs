namespace ClinicManagement.Validators.Auth
{
    public class UserModelValidator : ModelValidator
    {
        protected override Dictionary<string, string> ForeignKeyErrorMappings => new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["RoleId"] = "Role is required"
        };

        protected override Dictionary<string, string> DisplayNameMappings => new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["RoleId"] = "Role",
            ["FirstName"] = "First Name",
            ["MiddleName"] = "Middle Name",
            ["LastName"] = "Last Name",
            ["Email"] = "Email",
            ["PasswordHash"] = "Password",
            ["CreatedAt"] = "Created At"
        };

        protected override string[] NavigationPropertyIndicators => new[]
        {
            "Role",
            "PromotionRequests"
        };
    }
}

