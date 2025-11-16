using ClinicManagement.Data;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Controllers.Info
{
    public class DoctorOnCallStatusController : Controller
    {
        private readonly ClinicDbContext _context;

        public DoctorOnCallStatusController(ClinicDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
