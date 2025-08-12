namespace ClinicManagement.Models.Facilities
{
    public class Cabinet
    {
        public int Id { get; set; }
        public int Building { get; set; }
        public int Floor { get; set; }
        public int Number {  get; set; }
        public int TypeId { get; set; }

        //Many-to-one
        public CabinetType Type { get; set; } = null!;

        //One-to-many
        public ICollection<Schedule> Schedules { get; set; } = null!;

    }
}
