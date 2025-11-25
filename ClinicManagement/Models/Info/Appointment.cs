using ClinicManagement.Models.Facilities;
using ClinicManagement.Models.Health;
using ClinicManagement.Models.Humans;

namespace ClinicManagement.Models.Info
{
    public class Appointment
    {
        public int Id { get; set; }

        public int DoctorProcedureId { get; set; }

        
        public DoctorProcedure DoctorProcedure { get; set; } = null!;
        public int CabinetId { get; set; }

        
        public Cabinet Cabinet { get; set; } = null!;
        public int PatientId { get; set; }

        
        public Patient Patient { get; set; } = null!;
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public bool DidItHappen { get; set; }

        
        public Diagnosis? Diagnosis { get; set; }
    }
}
