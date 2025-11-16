namespace ClinicManagement.Models.Health
{
    public class SicknessProcedure
    {
        public int SicknessId { get; set; }

        //One-to-many
        public Sickness Sickness { get; set; } = null!;
        public int ProcedureId { get; set; }

        //One-to-many
        public Procedure Procedure { get; set; } = null!;

    }
}
