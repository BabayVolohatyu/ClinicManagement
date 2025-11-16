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
                    // valid token => authenticated user (Guest or other role from DB)
                    var identity = new ClaimsIdentity(principal.Claims, "Jwt");
                    context.User = new ClaimsPrincipal(identity);
                }
                else
                {
                    // invalid token => Unauthorized
                    SetUnauthorized(context);
                }
            }
            else
            {
                // no token => Unauthorized
                SetUnauthorized(context);
            }

            _logger.LogInformation("MIDDLEWARE: IsAuthenticated={Auth}, Type={Type}, RoleId={RoleId}",
                context.User?.Identity?.IsAuthenticated,
                context.User?.Identity?.AuthenticationType,
                context.User?.FindFirst("roleId")?.Value);

            await _next(context);
        }

        private static void SetUnauthorized(HttpContext context)
        {
            // Claims only to be able to inspect roleId if needed; leave authenticationType null -> IsAuthenticated == false
            var claims = new List<Claim>
    {
        new Claim("roleId", ((int)RoleType.Unauthorized).ToString()),
        new Claim(ClaimTypes.Role, RoleType.Unauthorized.ToString())
    };
            context.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
        }



    }

}