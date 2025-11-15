using ClinicManagement.Models.Humans;

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
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
    }
}
