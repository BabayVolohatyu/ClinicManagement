namespace ClinicManagement.Validators.Info
{
    public class AppointmentModelValidator : ModelValidator
    {
        protected override Dictionary<string, string> ForeignKeyErrorMappings => new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["DoctorProcedureId"] = "Doctor Procedure is required",
            ["CabinetId"] = "Cabinet is required",
            ["PatientId"] = "Patient is required"
        };

        protected override Dictionary<string, string> DisplayNameMappings => new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["DoctorProcedureId"] = "Doctor Procedure",
            ["CabinetId"] = "Cabinet",
            ["PatientId"] = "Patient",
            ["StartTime"] = "Start Time",
            ["EndTime"] = "End Time",
            ["DidItHappen"] = "Did It Happen"
        };

        protected override string[] NavigationPropertyIndicators => new[]
        {
            "DoctorProcedure",
            "Cabinet",
            "Patient",
            "Diagnosis"
        };
    }
}

