using ClinicManagement.Data;
using ClinicManagement.Models.Info;

namespace ClinicManagement.Controllers.Info
{
    public class AppointmentController : GenericController<Appointment>
    {
        public AppointmentController(ClinicDbContext context) : base(context) { }
    }
}
