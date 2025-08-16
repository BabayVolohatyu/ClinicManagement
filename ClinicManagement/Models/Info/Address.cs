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

        //One-to-many
        public ICollection<Patient> Patients { get; set; } = [];

        //One-to-many
        public ICollection<DoctorOnCallStatus> DoctorOnCallStatuses { get; set; } = [];


        //One-to-many
        public ICollection<HomeCallLog> HomeCallLogs { get; set; } = [];

    }
}
