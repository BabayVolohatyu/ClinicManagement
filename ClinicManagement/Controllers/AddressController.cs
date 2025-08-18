using ClinicManagement.Data;
using ClinicManagement.Models.Info;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Controllers
{
    [Route("[controller]")]
    public class AddressController : GenericController<Address>
    {
        public AddressController(ClinicDbContext context) : base(context) { }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Address address)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join("; ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return Content("Model is invalid: " + errors);
            }
            _context.Addresses.Add(address);
             await _context.SaveChangesAsync();
             return RedirectToAction(nameof(Index));
        }
    }
}
