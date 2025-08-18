using ClinicManagement.Data;
using ClinicManagement.Models.Health;

namespace ClinicManagement.Controllers.Health
{
    public class SicknessTreatmentController : GenericController<SicknessTreatment>
    {
        public SicknessTreatmentController(ClinicDbContext context) : base(context){ }
    }
}
