using ClinicManagement.Models.Humans;

namespace ClinicManagement.Models.Info
{
    public class Address
    {
        public int Id { get; set; }
        public string Country { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Locality { get; set; } = null!;
        public string StreetName { get; set; } = null!;
        public int StreetNumber { get; set; }

        public ICollection<Patient> Patients { get; set; } = new List<Patient>();
        public ICollection<DoctorOnCallStatus> DoctorOnCallStatuses { get; set; } = new List<DoctorOnCallStatus>();
        public ICollection<HomeCallLog> HomeCallLogs { get; set; } = new List<HomeCallLog>();


    }
}
