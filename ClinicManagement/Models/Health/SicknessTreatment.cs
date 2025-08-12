namespace ClinicManagement.Models.Health
{
    public class SicknessTreatment
    {
        public int SicknessId { get; set; }
        public Sickness Sickness { get; set; } = null!;
        public int TreatmentId { get; set; }
        public Treatment Treatment { get; set; } = null!;
    }
}
