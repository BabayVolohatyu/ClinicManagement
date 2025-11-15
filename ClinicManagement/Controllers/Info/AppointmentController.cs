using ClinicManagement.Data;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Controllers.Info
{
    public class AppointmentController : Controller
    {
        private readonly ClinicDbContext _context;

        public AppointmentController(ClinicDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
