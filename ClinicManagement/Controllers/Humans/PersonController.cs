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

        [HttpGet]
        public IActionResult Index()
        {
            var people = _context.People.ToList();
            return View("Views/Humans/Person/Index.cshtml", people);
        }
    }
}
