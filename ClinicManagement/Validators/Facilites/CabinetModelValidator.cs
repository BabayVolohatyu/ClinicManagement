namespace ClinicManagement.Validators.Facilites
{
    public class CabinetModelValidator : ModelValidator
    {
        protected override Dictionary<string, string> ForeignKeyErrorMappings => new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["TypeId"] = "Cabinet Type is required"
        };

        protected override Dictionary<string, string> DisplayNameMappings => new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["TypeId"] = "Cabinet Type",
            ["Building"] = "Building",
            ["Floor"] = "Floor",
            ["Number"] = "Number"
        };

        protected override string[] NavigationPropertyIndicators => new[]
        {
        "Type",
        "Cabinet",
        "Schedules",
        "Appointments"
    };
    }
}
