using ClinicManagement.Data;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Controllers.Facilities
{
    public class CabinetTypeController : Controller
    {
        private readonly ClinicDbContext _context;

        public CabinetTypeController(ClinicDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
