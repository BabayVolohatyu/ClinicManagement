using ClinicManagement.Data;
using ClinicManagement.Models.Info;

namespace ClinicManagement.Controllers.Info
{
    public class DoctorOnCallStatusController : GenericController<DoctorOnCallStatus>
    {
        public  DoctorOnCallStatusController(ClinicDbContext context) : base(context) { }
    }
}
