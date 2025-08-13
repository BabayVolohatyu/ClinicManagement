namespace ClinicManagement.Models.People
{
    public class DistrictDoctor
    {
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;
    }
}
