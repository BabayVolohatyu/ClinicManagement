using ClinicManagement.Models.People;
using ClinicManagement.Models.Facilities;

namespace ClinicManagement.Models.Info
{
    public class Schedule
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;

        public int CabinetId { get; set; }
        public Cabinet Cabinet { get; set; } = null!;

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
