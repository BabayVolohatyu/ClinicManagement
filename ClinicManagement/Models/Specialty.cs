namespace ClinicManagement.Models
{
    public class Specialty
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        //One-to-many
        public ICollection<Doctor> Doctors { get; set; } = null!;
    }
}
