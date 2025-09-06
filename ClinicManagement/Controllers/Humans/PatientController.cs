using ClinicManagement.Data;
using ClinicManagement.Models.Humans;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicManagement.Controllers.Humans
{
    public class PatientController : GenericController<Patient>
    {
        public PatientController(ClinicDbContext context) : base(context) { }


        [HttpGet]
        public override IActionResult Create()
        {
            var availablePeople = _context.People
                .Where(p => p.Patient == null)                               
                .ToList();

            ViewBag.PersonId = new SelectList(availablePeople, "Id", "Id");
            ViewBag.AddressId = new SelectList(_context.Addresses, "Id", "Id");
            return View();
        }
    }
}
