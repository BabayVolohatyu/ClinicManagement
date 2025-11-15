using ClinicManagement.Models;
using ClinicManagement.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ClinicManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IAuthService authService, ILogger<HomeController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!_authService.IsAuthenticated() && !_authService.IsGuest())
                return RedirectToAction("Index", "Login");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
