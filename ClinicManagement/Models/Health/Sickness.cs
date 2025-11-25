namespace ClinicManagement.Models.Health
{
    public class Sickness
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        
        public ICollection<SicknessSymptom> SicknessSymptoms { get; set; } = new List<SicknessSymptom>();
        
        public ICollection<SicknessTreatment> SicknessTreatment { get; set; } = new List<SicknessTreatment>();

        
        public ICollection<SicknessProcedure> SicknessProcedures { get; set; } = new List<SicknessProcedure>();
    }

}
