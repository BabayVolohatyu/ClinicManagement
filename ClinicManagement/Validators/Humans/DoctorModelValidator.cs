namespace ClinicManagement.Validators.Humans
{
    public class DoctorModelValidator : ModelValidator
    {
        protected override Dictionary<string, string> ForeignKeyErrorMappings => new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["PersonId"] = "Person is required",
            ["SpecialtyId"] = "Specialty is required"
        };

        protected override Dictionary<string, string> DisplayNameMappings => new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["PersonId"] = "Person",
            ["SpecialtyId"] = "Specialty"
        };

        protected override string[] NavigationPropertyIndicators => new[]
        {
            "Person",
            "Specialty",
            "DistrictDoctor",
            "OnCallStatus",
            "Schedules",
            "HomeCallLogs",
            "DoctorProcedures"
        };
    }
}

