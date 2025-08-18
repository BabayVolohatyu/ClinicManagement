using ClinicManagement.Data;
using ClinicManagement.Models.Info;

namespace ClinicManagement.Controllers.Info
{
    public class DoctorProcedureController : GenericController<DoctorProcedure>
    {
        public DoctorProcedureController(ClinicDbContext context) : base(context){ }
    }
}
