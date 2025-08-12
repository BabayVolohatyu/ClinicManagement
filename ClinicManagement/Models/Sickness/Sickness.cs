namespace ClinicManagement.Models.Sickness
{
    public class Sickness
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        //Many-to-many
        public ICollection<SicknessSymptom> SicknessSymptoms { get; set; } = null!;
    }

}
