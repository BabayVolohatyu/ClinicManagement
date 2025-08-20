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

            ViewBag.PersonId = new SelectList(_context.People, "Id", "Id");
            ViewBag.AddressId = new SelectList(_context.Addresses, "Id", "Id");
            return View();
        }
    }
}
