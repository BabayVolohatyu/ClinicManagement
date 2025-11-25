using ClinicManagement.Models.Info;

namespace ClinicManagement.Models.Facilities
{
    public class Cabinet
    {
        public int Id { get; set; }
        public int Building { get; set; }
        public int Floor { get; set; }
        public int Number { get; set; }
        public int TypeId { get; set; }

        
        public CabinetType Type { get; set; } = null!;

        
        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

        
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
}
