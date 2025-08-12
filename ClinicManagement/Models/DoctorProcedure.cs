using ClinicManagement.Models.Health;
using ClinicManagement.Models.Staff;

namespace ClinicManagement.Models
{
    public class DoctorProcedure
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        
        //One-to-many
        public Doctor Doctor { get; set; } = null!;
        public int ProcedureId { get; set; }

        //One-to-many
        public Procedure Procedure { get; set; } = null!;

        //Many-to-one
        public ICollection<Appointment> Appointments { get; set; } = null!;
    }
}
