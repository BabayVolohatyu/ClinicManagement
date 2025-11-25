using ClinicManagement.Models.Health;
using ClinicManagement.Models.Humans;

namespace ClinicManagement.Models.Info
{
    public class DoctorProcedure
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }

        
        public Doctor Doctor { get; set; } = null!;
        public int ProcedureId { get; set; }

        
        public Procedure Procedure { get; set; } = null!;

        
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
