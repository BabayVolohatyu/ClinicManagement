using ClinicManagement.Data;
using ClinicManagement.Models.Facilities;

namespace ClinicManagement.Controllers.Facilities
{
    public class CabinetTypeController : GenericController<CabinetType>
    {
        public CabinetTypeController(ClinicDbContext context) : base(context){ }
    }
}
