using ClinicManagement.Data;
using ClinicManagement.Models.Health;

namespace ClinicManagement.Controllers.Health
{
    public class SicknessSymptomController : GenericController<SicknessSymptom>
    {
        public SicknessSymptomController(ClinicDbContext context) : base(context) { }
    }
}
