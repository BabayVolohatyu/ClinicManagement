using ClinicManagement.Data;
using ClinicManagement.Models.Facilities;

namespace ClinicManagement.Controllers.Facilities
{
    public class CabinetController : GenericController<Cabinet>
    {
        public CabinetController(ClinicDbContext context) : base(context){ }
    }
}
