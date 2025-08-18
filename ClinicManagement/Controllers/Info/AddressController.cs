using ClinicManagement.Data;
using ClinicManagement.Models.Info;

namespace ClinicManagement.Controllers.Info
{
    public class AddressController : GenericController<Address>
    {
        public AddressController(ClinicDbContext context) : base(context) { }
    }
}
