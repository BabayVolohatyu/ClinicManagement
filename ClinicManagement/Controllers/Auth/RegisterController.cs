using ClinicManagement.Models.Auth;
using ClinicManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Controllers.Auth
{
    public class RegisterController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ILogger<RegisterController> _logger;

        public RegisterController(IAuthService authService, ILogger<RegisterController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        { 
            return View("Register");
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return View("Register", model);

            if (model.Password != model.ConfirmPassword)
            {
                TempData["Error"] = "Passwords do not match.";
                return View("Register", model);
            }

            var success = await _authService.RegisterAsync(model);
            if (!success)
            {
                TempData["Error"] = "User with this email already exists.";
                return View("Register", model);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
