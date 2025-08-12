using ClinicManagement.Models.Staff;

namespace ClinicManagement.Models
{
    public class HomeCallLog
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }

        //Many-to-one
        public Doctor Doctor { get; set; } = null!;
        public int AddressId { get; set; }
        //Many-to-one
        public Address Address { get; set; } = null!;
        public DateTime DateTime { get; set; }
    }
}
