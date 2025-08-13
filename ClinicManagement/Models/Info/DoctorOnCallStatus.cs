using ClinicManagement.Models.People;

namespace ClinicManagement.Models.Info
{
    public class DoctorOnCallStatus
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        
        //One-to-one
        public Doctor Doctor { get; set; } = null!;
        public int AddressId { get; set; }
        //Many-to-one
        public Address Address { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
