using ClinicManagement.Data;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Controllers.Health
{
    public class SicknessProcedureController : Controller
    {
        private readonly ClinicDbContext _context;

        public SicknessProcedureController(ClinicDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
