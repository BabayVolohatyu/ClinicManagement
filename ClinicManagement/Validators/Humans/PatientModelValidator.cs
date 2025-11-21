namespace ClinicManagement.Validators.Humans
{
    public class PatientModelValidator : ModelValidator
    {
        protected override Dictionary<string, string> ForeignKeyErrorMappings => new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["PersonId"] = "Person is required",
            ["AddressId"] = "Address is required"
        };

        protected override Dictionary<string, string> DisplayNameMappings => new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["PersonId"] = "Person",
            ["AddressId"] = "Address"
        };

        protected override string[] NavigationPropertyIndicators => new[]
        {
            "Person",
            "Address",
            "Appointments"
        };
    }
}

