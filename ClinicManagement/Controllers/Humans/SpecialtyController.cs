using ClinicManagement.Data;
using ClinicManagement.Models.Humans;

namespace ClinicManagement.Controllers.Humans
{
    public class SpecialtyController : GenericController<Specialty>
    {
        public SpecialtyController(ClinicDbContext context) : base(context) { }
    }
}
