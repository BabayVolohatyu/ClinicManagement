using ClinicManagement.Models.Health;
using ClinicManagement.Services;

namespace ClinicManagement.Controllers.Health
{
    public class SicknessController : CommonController<Sickness>
    {
        public SicknessController(IService<Sickness> service, ILogger<SicknessController> logger)
            : base(service, logger) { }
    }
}
