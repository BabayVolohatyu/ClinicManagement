using ClinicManagement.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ClinicManagement.Helpers
{
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly RoleType[] _allowedRoles;

        public AuthorizeAttribute(params RoleType[] roles)
        {
            _allowedRoles = roles;
        }

        public AuthorizeAttribute()
        {
            _allowedRoles = Array.Empty<RoleType>();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            // Check if user is authenticated at all
            if (user?.Identity?.IsAuthenticated != true)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }

            // If no specific roles required, allow any authenticated user
            if (_allowedRoles.Length == 0)
            {
                return; // Allow access to any authenticated user
            }

            // Check if user has one of the allowed roles
            var roleIdClaim = user.FindFirst("roleId");
            if (roleIdClaim == null || !int.TryParse(roleIdClaim.Value, out int userRoleId))
            {
                context.Result = new ForbidResult();
                return;
            }

            var userRoleType = (RoleType)userRoleId;
            if (!_allowedRoles.Contains(userRoleType))
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}