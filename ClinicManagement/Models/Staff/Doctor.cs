namespace ClinicManagement.Models.Staff
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

        //One-to-one
        public DistrictDoctor? DistrictDoctor { get; set; }

        //One-to-many
        public ICollection<Schedule> Schedules { get; set; } = null!;
    }
}
