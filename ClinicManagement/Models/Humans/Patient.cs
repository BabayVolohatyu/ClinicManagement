using ClinicManagement.Models.Info;

namespace ClinicManagement.Models.Humans
{
    public class Patient
    {
        public int Id { get; set; }
        public int PersonId {  get; set; }
        //One-to-one
        public Person Person { get; set; } = null!;
        public int AddressId { get; set; }

        //Many-to-one
        public Address Address { get; set; } = null!;
    }
}
