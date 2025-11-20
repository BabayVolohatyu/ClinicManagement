using ClinicManagement.Data;
using ClinicManagement.Models.Humans;

namespace ClinicManagement.Services.Humans
{
    public class PersonService : Service<Person>
    {
        public PersonService(ClinicDbContext context, ILogger<PersonService> logger)
        : base(context, logger) { }
    }
}
