using ClinicManagement.Data;
using ClinicManagement.Models.Info;

namespace ClinicManagement.Controllers.Info
{
    public class ScheduleController : GenericController<Schedule>
    {
        public ScheduleController(ClinicDbContext context) : base(context) { }
    }
}
