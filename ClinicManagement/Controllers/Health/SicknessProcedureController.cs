using ClinicManagement.Data;
using ClinicManagement.Models.Health;

namespace ClinicManagement.Controllers.Health
{
    public class SicknessProcedureController : GenericController<SicknessProcedure>
    {
        public SicknessProcedureController(ClinicDbContext context) : base(context){ }
    }
}
