using ClinicManagement.Data;
using ClinicManagement.Models.Info;

namespace ClinicManagement.Services.Info
{
    public class AddressService : Service<Address>
    {
        public AddressService(ClinicDbContext context, ILogger<AddressService> logger)
        : base(context, logger) { }
    }
}

