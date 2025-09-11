using ClinicManagement.Data;
using ClinicManagement.Models.Health;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Controllers.Health
{
    public class SymptomController : Controller
    {
        private readonly ClinicDbContext _context;

        public SymptomController(ClinicDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
