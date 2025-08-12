namespace ClinicManagement.Models.Health
{
    public class Sickness
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        //Many-to-many
        public ICollection<SicknessSymptom> SicknessSymptoms { get; set; } = null!;
        //Many-to-many
        public ICollection<SicknessTreatment> SicknessTreatment { get; set; } = null!;
    }

}
