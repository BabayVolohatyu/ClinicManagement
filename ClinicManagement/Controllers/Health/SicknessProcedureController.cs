using ClinicManagement.Data;
using ClinicManagement.Models.Health;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Controllers.Health
{
    public class SicknessProcedureController : GenericController<SicknessProcedure>
    {
        private readonly ClinicDbContext _context;

        public SicknessProcedureController(ClinicDbContext context) : base(context) { }

        public IActionResult Index()
        {
            return View();
        }
    }
}
