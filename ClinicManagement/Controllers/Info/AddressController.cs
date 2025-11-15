using ClinicManagement.Data;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Controllers.Info
{
    public class AddressController : Controller
    {
        private readonly ClinicDbContext _context;

        public AddressController(ClinicDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
