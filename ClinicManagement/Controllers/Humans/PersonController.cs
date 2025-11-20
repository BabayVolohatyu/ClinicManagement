using ClinicManagement.Models.Humans;
using ClinicManagement.Services;

namespace ClinicManagement.Controllers.Humans
{
    public class PersonController : CommonController<Person>
    {
        public PersonController(IService<Person> service, ILogger<PersonController> logger)
            : base(service, logger) { }
    }
}
