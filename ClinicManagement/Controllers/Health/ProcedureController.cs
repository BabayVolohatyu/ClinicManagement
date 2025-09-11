using ClinicManagement.Data;
using ClinicManagement.Models.Health;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Controllers.Health
{
    public class ProcedureController : Controller
    {
        private readonly ClinicDbContext _context;

        public ProcedureController(ClinicDbContext context)  
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
