namespace ClinicManagement.Validators.Info
{
    public class ScheduleModelValidator : ModelValidator
    {
        protected override Dictionary<string, string> ForeignKeyErrorMappings => new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["DoctorId"] = "Doctor is required",
            ["CabinetId"] = "Cabinet is required"
        };

        protected override Dictionary<string, string> DisplayNameMappings => new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["DoctorId"] = "Doctor",
            ["CabinetId"] = "Cabinet",
            ["StartTime"] = "Start Time",
            ["EndTime"] = "End Time"
        };

        protected override string[] NavigationPropertyIndicators => new[]
        {
            "Doctor",
            "Cabinet"
        };
    }
}

