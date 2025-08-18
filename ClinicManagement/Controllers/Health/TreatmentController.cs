using ClinicManagement.Data;
using ClinicManagement.Models.Health;

namespace ClinicManagement.Controllers.Health
{
    public class TreatmentController : GenericController<Treatment>
    {
        public TreatmentController(ClinicDbContext context) : base(context){ }
    }
}
