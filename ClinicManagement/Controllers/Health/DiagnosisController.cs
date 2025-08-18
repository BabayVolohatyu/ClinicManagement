using ClinicManagement.Data;
using ClinicManagement.Models.Health;

namespace ClinicManagement.Controllers.Health
{
    public class DiagnosisController : GenericController<Diagnosis>
    {
        public DiagnosisController(ClinicDbContext context) : base(context){  }
    }
}
