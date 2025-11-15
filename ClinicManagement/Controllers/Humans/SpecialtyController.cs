using ClinicManagement.Data;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Controllers.Humans
{
    public class SpecialtyController : Controller
    {
        private readonly ClinicDbContext _context;

        public SpecialtyController(ClinicDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
