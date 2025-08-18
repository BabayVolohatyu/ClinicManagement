using ClinicManagement.Data;
using ClinicManagement.Models.Health;

namespace ClinicManagement.Controllers.Health
{
    public class SicknessController : GenericController<Sickness>
    { 
        public SicknessController(ClinicDbContext context) : base(context) { }
    }
}
