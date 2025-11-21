using ClinicManagement.Models.Health;
using ClinicManagement.Services;

namespace ClinicManagement.Controllers.Health
{
    public class SymptomController : CommonController<Symptom>
    {
        public SymptomController(IService<Symptom> service, ILogger<SymptomController> logger)
            : base(service, logger) { }
    }
}
