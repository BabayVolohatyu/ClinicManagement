namespace ClinicManagement.Models.Health
{
    public class Sickness
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        //Many-to-one
        public ICollection<SicknessSymptom> SicknessSymptoms { get; set; } = new List<SicknessSymptom>();
        //Many-to-one
        public ICollection<SicknessTreatment> SicknessTreatment { get; set; } = new List<SicknessTreatment>();

        //Many-to-one
        public ICollection<SicknessProcedure> SicknessProcedures { get; set; } = new List<SicknessProcedure>();
    }

}
