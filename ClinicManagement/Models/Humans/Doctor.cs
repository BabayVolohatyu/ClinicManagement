using ClinicManagement.Models.Info;

namespace ClinicManagement.Models.Humans
{
    public class Doctor
    {
        public int Id { get; set; }
        public int PersonId { get; set; }

        
        public Person Person { get; set; } = null!;
        public int SpecialtyId { get; set; }

        
        public Specialty Specialty { get; set; } = null!;

        
        public DistrictDoctor? DistrictDoctor { get; set; }

        
        public DoctorOnCallStatus? OnCallStatus { get; set; }

        
        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

        
        public ICollection<HomeCallLog> HomeCallLogs { get; set; } = new List<HomeCallLog>();

        
        public ICollection<DoctorProcedure> DoctorProcedures { get; set; } = new List<DoctorProcedure>();
    }
}
