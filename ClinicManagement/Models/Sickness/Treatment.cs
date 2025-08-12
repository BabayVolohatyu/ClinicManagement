namespace ClinicManagement.Models.Sickness
{
    public class Treatment
    {
        public  int Id { get; set; }
        public string Name { get; set; } = null!;

        //Many-to-many
        public ICollection<SicknessTreatment> SicknessTreatment { get; set; } = null!;
    }
}
