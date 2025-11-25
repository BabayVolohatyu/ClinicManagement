namespace ClinicManagement.Models.Facilities
{
    public class CabinetType
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;

        
        public ICollection<Cabinet> Cabinets { get; set; } = new List<Cabinet>();
    }
}
