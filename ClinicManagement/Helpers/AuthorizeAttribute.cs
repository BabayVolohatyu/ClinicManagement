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
            // If no roles specified, allow any authenticated user
            _allowedRoles = new RoleType[] {
                RoleType.Guest,
                RoleType.Authorized,
                RoleType.Operator,
                RoleType.Admin
            };
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user?.Identity?.IsAuthenticated != true)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }

            // Check if user has one of the allowed roles
            if (_allowedRoles.Length > 0)
            {
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
}