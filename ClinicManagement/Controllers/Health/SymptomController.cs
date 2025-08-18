using ClinicManagement.Data;
using ClinicManagement.Models.Health;

namespace ClinicManagement.Controllers.Health
{
    public class SymptomController : GenericController<Symptom>
    {
        public SymptomController(ClinicDbContext context) : base(context) { }
    }
}
