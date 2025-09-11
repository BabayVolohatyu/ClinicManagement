using ClinicManagement.Data;
using ClinicManagement.Models.Health;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Controllers.Health
{
    public class SicknessSymptomController : Controller
    {
        private readonly ClinicDbContext _context;

        public SicknessSymptomController(ClinicDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
