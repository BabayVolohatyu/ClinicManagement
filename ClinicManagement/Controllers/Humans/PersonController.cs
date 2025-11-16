using ClinicManagement.Data;
using ClinicManagement.Models.Humans;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Controllers.Humans
{
    public class PersonController : Controller
    {
        private readonly ClinicDbContext _context;

        private const string _route = "Views/Humans/Person/";
        public PersonController(ClinicDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var people = await _context.People.ToListAsync();

            return View(ViewPath("Index"), people);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var person = await _context.People.FindAsync(id);
            return View(ViewPath("Details"), person);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Person person)
        {
            _context.People.Update(person);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var person = await _context.People.FindAsync(id);
            if (person != null)
            {
                _context.People.Remove(person);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(ViewPath("Create"));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Person person)
        {
            await _context.People.AddAsync(person);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private string ViewPath(string viewName) => _route + viewName + ".cshtml";

    }
}
