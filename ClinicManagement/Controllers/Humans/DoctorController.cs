using ClinicManagement.Data;
using ClinicManagement.Models.Humans;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Controllers.Humans
{
    public class DoctorController : Controller
    {
        private readonly ClinicDbContext _context;

        public DoctorController(ClinicDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
