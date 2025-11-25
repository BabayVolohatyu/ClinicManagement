using ClinicManagement.Models.Info;

namespace ClinicManagement.Models.Humans
{
    public class Patient
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        
        public Person? Person { get; set; }
        public int AddressId { get; set; }

        
        public Address? Address { get; set; }

        
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
