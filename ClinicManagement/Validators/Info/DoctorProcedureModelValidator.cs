namespace ClinicManagement.Validators.Info
{
    public class DoctorProcedureModelValidator : ModelValidator
    {
        protected override Dictionary<string, string> ForeignKeyErrorMappings => new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["DoctorId"] = "Doctor is required",
            ["ProcedureId"] = "Procedure is required"
        };

        protected override Dictionary<string, string> DisplayNameMappings => new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["DoctorId"] = "Doctor",
            ["ProcedureId"] = "Procedure"
        };

        protected override string[] NavigationPropertyIndicators => new[]
        {
            "Doctor",
            "Procedure",
            "Appointments"
        };
    }
}

