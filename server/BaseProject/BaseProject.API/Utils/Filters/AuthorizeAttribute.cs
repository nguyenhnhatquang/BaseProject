using BaseProject.Domain.Entities;
using BaseProject.Domain.Shares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BaseProject.API.Utils.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly IList<AccountPermission> _roles;

    public AuthorizeAttribute(params AccountPermission[]? roles)
    {
        _roles = roles ?? [];
    }
    
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Check if the action allows anonymous access
        var isAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
    
        // If anonymous access is allowed, skip authorization
        if (isAnonymous)
            return;
    
        // Authorize the request
        if (context.HttpContext.Items["Account"] is not Account account || (_roles.Any() && account.AccountRoles.Any(rl => _roles.Contains(rl.Role.Type))))
        {
            context.Result = new JsonResult(new Error("Error.Authorization", "Xin lỗi bạn không có quyền truy cập."))
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
        }
    }
}