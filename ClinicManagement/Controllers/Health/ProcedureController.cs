using ClinicManagement.Data;
using ClinicManagement.Models.Health;

namespace ClinicManagement.Controllers.Health
{
    public class ProcedureController : GenericController<Procedure>
    {
        public ProcedureController(ClinicDbContext context) : base(context) { }
    }
}
