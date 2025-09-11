using ClinicManagement.Data;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Controllers.Facilities
{
    public class CabinetController : Controller
    {
        private readonly ClinicDbContext _context;

        public CabinetController(ClinicDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
