namespace ClinicManagement.Models.Doctor
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Patronymic { get; set; } = null!;

        public int SpecialtyId { get; set; }

        //Many-to-one
        public Specialty Specialty { get; set; } = null!;
    }
}
