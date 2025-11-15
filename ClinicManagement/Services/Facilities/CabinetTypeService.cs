using ClinicManagement.Data;
using ClinicManagement.Models.Facilities;

namespace ClinicManagement.Services.Facilities
{
    public class CabinetTypeService : Service<CabinetType>
    {
        public CabinetTypeService(ClinicDbContext context, ILogger<CabinetTypeService> logger)
            : base(context, logger) { }
    }

}
