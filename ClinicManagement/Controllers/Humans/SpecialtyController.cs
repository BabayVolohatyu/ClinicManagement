using ClinicManagement.Models.Humans;
using ClinicManagement.Services;

namespace ClinicManagement.Controllers.Humans
{
    public class SpecialtyController : CommonController<Specialty>
    {
        public SpecialtyController(IService<Specialty> service, ILogger<SpecialtyController> logger)
           : base(service, logger) { }
    }
}
