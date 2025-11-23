using ClinicManagement.Models.Health;
using ClinicManagement.Services;

namespace ClinicManagement.Controllers.Health
{
    public class TreatmentController : CommonController<Treatment>
    {
        public TreatmentController(IService<Treatment> service, ILogger<TreatmentController> logger)
            : base(service, logger) { }
    }
}
