namespace ClinicManagement.Models.Health
{
    public class Treatment
    {
        public  int Id { get; set; }
        public string Name { get; set; } = null!;

        //Many-to-many
        public ICollection<SicknessTreatment> SicknessTreatment { get; set; } = new List<SicknessTreatment>();
    }
}
