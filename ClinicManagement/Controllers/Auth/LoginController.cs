using ClinicManagement.Models.Auth;
using ClinicManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Controllers.Auth
{
    public class LoginController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IAuthService authService, ILogger<LoginController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View("Login", model);

            var success = await _authService.LoginAsync(model.Email, model.Password);
            if (!success)
            {
                TempData["Error"] = "Invalid email or password.";
                return View("Login", model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Guest()
        {
            _authService.SetGuest();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            _authService.Logout();
            return RedirectToAction("Index", "Login");
        }
    }
}
