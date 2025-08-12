namespace ClinicManagement.Models.Cabinet
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

    }
}
