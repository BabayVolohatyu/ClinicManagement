using ClinicManagement.Data;
using ClinicManagement.Models.Info;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Controllers.Info
{
    public class DoctorProcedureController : Controller
    {
        private readonly ClinicDbContext _context;

        public DoctorProcedureController(ClinicDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
