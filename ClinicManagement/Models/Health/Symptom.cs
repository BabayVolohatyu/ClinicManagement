namespace ClinicManagement.Models.Health
{
    public class Symptom
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        
        public ICollection<SicknessSymptom> SicknessSymptoms { get; set; } = new List<SicknessSymptom>();
    }
}
