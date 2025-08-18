using ClinicManagement.Data;
using ClinicManagement.Models.Humans;

namespace ClinicManagement.Controllers.Humans
{
    public class DistrictDoctorController : GenericController<DistrictDoctor>
    {
        public DistrictDoctorController(ClinicDbContext context) : base(context) { }
    }
}
