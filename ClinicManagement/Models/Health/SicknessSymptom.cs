namespace ClinicManagement.Models.Health
{
    public class SicknessSymptom
    {
        public int SicknessId { get; set; }
        public Sickness Sickness { get; set; } = null!;
        public int SymptomId { get; set; }
        public Symptom Symptom { get; set; } = null!;
    }
}
