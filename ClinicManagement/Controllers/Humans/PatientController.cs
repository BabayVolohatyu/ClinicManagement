using ClinicManagement.Data;
using ClinicManagement.Models.Humans;

namespace ClinicManagement.Controllers.Humans
{
    public class PatientController : GenericController<Patient>
    {
        public PatientController(ClinicDbContext context) : base(context) { }
    }
}
