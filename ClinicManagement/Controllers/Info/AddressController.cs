using ClinicManagement.Data;
using ClinicManagement.Models.Info;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Controllers.Info
{
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
            _context.Addresses.Add(address);
             await _context.SaveChangesAsync();
             return RedirectToAction(nameof(Index));
        }
    }
}
