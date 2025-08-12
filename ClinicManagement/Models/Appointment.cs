using ClinicManagement.Models.Facilities;
using ClinicManagement.Models.Health;
using Npgsql;

namespace ClinicManagement.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        
        public int DoctorProcedureId { get; set; }

        //One-to-many
        public DoctorProcedure DoctorProcedure { get; set; } = null!;
        public int CabinetId { get; set; }
        
        //Many-to-one
        public Cabinet Cabinet { get; set; } = null!;
        public int PatientId {  get; set; }

        //Many-to-one
        public Patient Patient { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool DidItHappen {  get; set; }

        //One-to-one
        public Diagnosis? Diagnosis { get; set; }
    }
}
