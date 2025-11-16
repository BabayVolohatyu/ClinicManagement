using ClinicManagement.Models.Auth;
using ClinicManagement.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            Response.Cookies.Delete("jwt", new CookieOptions
            {
                Path = "/",
                Secure = false,
                HttpOnly = true,
                SameSite = SameSiteMode.Lax
            });

            SetUnauthorizedUser();

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

        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt", new CookieOptions
            {
                Path = "/",
                Secure = false,
                HttpOnly = true,
                SameSite = SameSiteMode.Lax
            });

            return RedirectToAction("Login", "Auth");
        }
        public IActionResult ForgotPassword()
        {
            throw new NotImplementedException("Not implemented yet.");
        }


        private void SetUnauthorizedUser()
        {
            var claims = new List<Claim>
            {
                new Claim("roleId", ((int)RoleType.Unauthorized).ToString()),
                new Claim(ClaimTypes.Role, RoleType.Unauthorized.ToString())
            };

            HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "Unauthorized"));
        }
    }
}

