namespace ClinicManagement.Models
{
    public class Symptom
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        
        //Many-to-Many
        public ICollection<SicknessSymptom> SicknessSymptoms { get; set; } = null!;
    }
}
