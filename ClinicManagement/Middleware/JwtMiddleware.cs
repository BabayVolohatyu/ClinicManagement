using ClinicManagement.Models.Auth;
using ClinicManagement.Services;
using System.Security.Claims;

namespace ClinicManagement.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<JwtMiddleware> _logger;
        private readonly IServiceProvider _serviceProvider;

        public JwtMiddleware(RequestDelegate next, ILogger<JwtMiddleware> logger, IServiceProvider serviceProvider)
        {
            _next = next;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            var jwtService = context.RequestServices.GetRequiredService<IJwtService>();
            var token = context.Request.Cookies["jwt"];

            if (!string.IsNullOrEmpty(token))
            {
                var principal = jwtService.Validate(token);
                if (principal != null)
                {
                    context.User = principal;
                }
                else
                {
                    // Invalid token: fallback to unauthorized
                    SetUnauthorizedUser(context);
                }
            }
            else
            {
                // No cookie = unauthorized
                SetUnauthorizedUser(context);
            }

            await _next(context);
        }

        private void SetUnauthorizedUser(HttpContext context)
        {
            var claims = new List<Claim>
    {
        new Claim("roleId", ((int)RoleType.Unauthorized).ToString()),
        new Claim(ClaimTypes.Role, RoleType.Unauthorized.ToString())
    };

            context.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "Unauthorized"));
        }


    }

}