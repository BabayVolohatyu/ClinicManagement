using ClinicManagement.Controllers.Base;
using ClinicManagement.Models.Facilities;
using ClinicManagement.Services;

namespace ClinicManagement.Controllers.Facilities
{
    public class CabinetTypeController : CommonController<CabinetType>
    {
        public CabinetTypeController(IService<CabinetType> service, ILogger<CabinetTypeController> logger)
            : base(service, logger) { }
    }
}
