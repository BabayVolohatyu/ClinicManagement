namespace ClinicManagement.Models
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
        public ICollection<Patient> Patients { get; set; } = null!;

        //One-to-many
        public ICollection<DoctorOnCallStatus> DoctorOnCallStatuses { get; set; } = null!;


        //One-to-many
        public ICollection<HomeCallLog> HomeCallLogs { get; set; } = null!;

    }
}
