using ClinicManagement.Data;
using ClinicManagement.Models.Info;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Controllers
{
    [Route("/address")]
    public class AddressController : Controller
    {
        private readonly ClinicDbContext _context;
        public AddressController(ClinicDbContext context)
        {
            _context = context;
        }
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
             return RedirectToRoute(new {modelName = nameof(Address).ToLower()});
        }
    }
}
