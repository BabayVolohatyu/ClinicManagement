using ClinicManagement.Services;
using System.Security.Claims;

namespace ClinicManagement.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<JwtMiddleware> _logger;

        public JwtMiddleware(RequestDelegate next, ILogger<JwtMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IJwtService jwtService)
        {
            var token = context.Request.Cookies["jwt"];

            if (!string.IsNullOrEmpty(token))
            {
                var principal = jwtService.Validate(token);
                if (principal != null)
                {
                    context.User = principal;
                    _logger.LogDebug("User authenticated: {UserId}",
                        principal.FindFirstValue("id"));
                }
                else
                {
                    _logger.LogDebug("Invalid JWT token, clearing cookie");
                    context.Response.Cookies.Delete("jwt");
                }
            }

            await _next(context);
        }
    }
}