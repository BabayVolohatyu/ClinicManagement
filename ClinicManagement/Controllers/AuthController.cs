using ClinicManagement.Models.Auth;
using ClinicManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _auth;
        private readonly IJwtService _jwt;

        public AuthController(IAuthService auth, IJwtService jwt)
        {
            _auth = auth;
            _jwt = jwt;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("AuthPage");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _auth.LoginAsync(model);

            if (user == null)
            {
                ViewBag.LoginFailed = true;
                return View("AuthPage");
            }

            var token = _jwt.Generate(user);
            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax
            });


            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            try
            {
                var user = await _auth.RegisterAsync(model);

                var token = _jwt.Generate(user);
                Response.Cookies.Append("jwt", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Lax
                });


                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.RegisterError = ex.Message;
                return View("AuthPage");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Guest()
        {
            var user = await _auth.GuestLoginAsync();
            var token = _jwt.Generate(user);

            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax
            });

            return RedirectToAction("Index", "Home");
        }


        public IActionResult ForgotPassword()
        {
            throw new NotImplementedException("Not implemented yet.");
        }
    }
}

