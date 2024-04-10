using Microsoft.AspNetCore.Mvc.Filters;

namespace BaseProject.API.Utils.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Check if the action allows anonymous access
        var isAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
    
        // If anonymous access is allowed, skip authorization
        if (isAnonymous)
            return;
    
        // Authorize the request
        // TODO: Add your authorization logic here
    }
}