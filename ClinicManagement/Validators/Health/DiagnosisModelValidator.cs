namespace ClinicManagement.Validators.Health
{
    public class DiagnosisModelValidator : ModelValidator
    {
        protected override Dictionary<string, string> ForeignKeyErrorMappings => new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["AppointmentId"] = "Appointment is required"
        };

        protected override Dictionary<string, string> DisplayNameMappings => new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["AppointmentId"] = "Appointment",
            ["Prescription"] = "Prescription"
        };

        protected override string[] NavigationPropertyIndicators => new[]
        {
            "Appointment"
        };
    }
}

