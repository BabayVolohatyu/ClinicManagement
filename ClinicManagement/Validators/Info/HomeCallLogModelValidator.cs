namespace ClinicManagement.Validators.Info
{
    public class HomeCallLogModelValidator : ModelValidator
    {
        protected override Dictionary<string, string> ForeignKeyErrorMappings => new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["DoctorId"] = "Doctor is required",
            ["AddressId"] = "Address is required"
        };

        protected override Dictionary<string, string> DisplayNameMappings => new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["DoctorId"] = "Doctor",
            ["AddressId"] = "Address",
            ["DateTime"] = "Date Time"
        };

        protected override string[] NavigationPropertyIndicators => new[]
        {
            "Doctor",
            "Address"
        };
    }
}

