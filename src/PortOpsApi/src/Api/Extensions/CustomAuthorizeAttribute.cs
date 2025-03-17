using Microsoft.AspNetCore.Mvc.Filters;

namespace Sonasid.DisUp.PortOps.Api.Extensions;
public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly string[] _allowedRoles;

    public CustomAuthorizeAttribute(params string[] roles)
    {
        _allowedRoles = roles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var r = _allowedRoles.FirstOrDefault();
        var rolesClaim = context.HttpContext.User.FindFirst("Roles")?.Value;

        if (rolesClaim == null)
        {
            context.Result = new ForbidResult();
            return;
        }

        var roles = rolesClaim.Split(',').Select(r => r.Trim());

        if (!_allowedRoles.Any(role => roles.Contains(role)))
        {
            context.Result = new ForbidResult();
        }
    }
}
