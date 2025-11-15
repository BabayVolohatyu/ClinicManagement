using ClinicManagement.Data;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Controllers.Info
{
    public class ScheduleController : Controller
    {
        private readonly ClinicDbContext _context;

        public ScheduleController(ClinicDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
