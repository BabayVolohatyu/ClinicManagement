namespace ClinicManagement.Models.Health
{
    public class SicknessProcedure
    {
        public int SicknessId { get; set; }

        
        public Sickness Sickness { get; set; } = null!;
        public int ProcedureId { get; set; }

        
        public Procedure Procedure { get; set; } = null!;

    }
}
