using ClinicManagement.Data;
using ClinicManagement.Models.Humans;

namespace ClinicManagement.Controllers.Humans
{
    public class DoctorController : GenericController<Doctor>
    {
        public DoctorController(ClinicDbContext context) : base(context){ }
    }
}
