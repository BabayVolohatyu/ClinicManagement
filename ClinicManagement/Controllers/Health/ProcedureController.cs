using ClinicManagement.Models.Health;
using ClinicManagement.Services;

namespace ClinicManagement.Controllers.Health
{
    public class ProcedureController : CommonController<Procedure>
    {
        public ProcedureController(IService<Procedure> service, ILogger<ProcedureController> logger)
            : base(service, logger) { }
    }
}
