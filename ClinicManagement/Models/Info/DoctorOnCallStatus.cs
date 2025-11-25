using ClinicManagement.Models.Humans;

namespace ClinicManagement.Models.Info
{
    public class DoctorOnCallStatus
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }

        
        public Doctor Doctor { get; set; } = null!;
        public int AddressId { get; set; }
        
        public Address Address { get; set; } = null!;
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
    }
}
