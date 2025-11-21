using ClinicManagement.Models.Info;
using ClinicManagement.Services;

namespace ClinicManagement.Controllers.Info
{
    public class AddressController : CommonController<Address>
    {
        public AddressController(IService<Address> service, ILogger<AddressController> logger)
            : base(service, logger) { }
    }
}
