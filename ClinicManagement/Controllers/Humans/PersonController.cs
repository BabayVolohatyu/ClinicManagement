using ClinicManagement.Data;
using ClinicManagement.Models.Humans;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Controllers.Humans
{
    public class PersonController : Controller
    {
        private readonly ClinicDbContext _context;

        public PersonController(ClinicDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
