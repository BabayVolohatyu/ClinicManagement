namespace ClinicManagement.Models.Facilities
{
    public class CabinetType
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;

        //One-to-many(Configured in Cabinet entity config)
        public ICollection<Cabinet> Cabinets { get; set; } = null!;
    }
}
