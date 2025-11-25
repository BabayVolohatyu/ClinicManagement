using ClinicManagement.Models.Auth;
using ClinicManagement.Services;
using ClinicManagement.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ClinicManagement.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _auth;
        private readonly IJwtService _jwt;
        private readonly IPasswordChangeRequestService _passwordChangeRequestService;

        public AuthController(IAuthService auth, IJwtService jwt, IPasswordChangeRequestService passwordChangeRequestService)
        {
            _auth = auth;
            _jwt = jwt;
            _passwordChangeRequestService = passwordChangeRequestService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            DeleteCookies();

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

            AppendCookies(token);

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult LoginAsGuest()
        {
            DeleteCookies();

            SetUnauthorizedUser();

            return RedirectToAction("Login", "Auth");
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            try
            {
                var user = await _auth.RegisterAsync(model);

                var token = _jwt.Generate(user);
                AppendCookies(token);

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

            AppendCookies(token);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            DeleteCookies();

            return RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            
            if (model.NewPassword != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                return View(model);
            }

            var user = await _auth.GetUserByEmailAsync(model.Email);
            
            
            if (user != null)
            {
                try
                {
                    
                    var passwordHash = HashPassword(model.NewPassword);

                    var passwordChangeRequest = new PasswordChangeRequest
                    {
                        UserId = user.Id,
                        RequestedPasswordHash = passwordHash,
                        RequestedAt = DateTimeOffset.UtcNow,
                        Status = PasswordChangeStatus.Pending
                    };

                    await _passwordChangeRequestService.AddAsync(passwordChangeRequest);
                }
                catch (Exception)
                {
                    
                }
            }

            
            ViewBag.SuccessMessage = "If an account with that email exists, a password change request has been submitted. An administrator will process your request shortly.";

            return View(model);
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
        private void AppendCookies(string token)
        {
            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                Path = "/",
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax
            });
        }
        private void DeleteCookies()
        {
            Response.Cookies.Delete("jwt", new CookieOptions
            {
                Path = "/",
                Secure = false,
                HttpOnly = true,
                SameSite = SameSiteMode.Lax
            });
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}

