using ClinicManagement.Data;
using ClinicManagement.Models.Humans;

namespace ClinicManagement.Controllers.Humans
{
    public class PersonController : GenericController<Person>
    {
        public PersonController(ClinicDbContext context) : base(context){ }
    }
}
