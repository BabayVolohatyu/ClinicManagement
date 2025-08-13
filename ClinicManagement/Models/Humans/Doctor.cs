using ClinicManagement.Models.Info;

namespace ClinicManagement.Models.Humans
{
    public class Doctor
    {
        public int Id { get; set; }
        public int PersonId { get; set; }

        //One-to-one
        public Person Person { get; set; } = null!;
        public int SpecialtyId { get; set; }

        //Many-to-one
        public Specialty Specialty { get; set; } = null!;

        //One-to-one
        public DistrictDoctor? DistrictDoctor { get; set; }

        //One-to-one
        public DoctorOnCallStatus? OnCallStatus { get; set; }

        //One-to-many
        public ICollection<Schedule> Schedules { get; set; } = null!;

        //One-to-many
        public ICollection<HomeCallLog> HomeCallLogs { get; set; } = null!;

        //Many-to-one
        public ICollection<DoctorProcedure> DoctorProcedures { get; set; } = null!;
    }
}
