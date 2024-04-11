using BaseProject.Infrastructure;
using BaseProject.Infrastructure.Authorization.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.API.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    
    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext context, IJwtUtils jwtUtils, ApplicationDbContext dbContext)
    {
        var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();

        var accountId = jwtUtils.ValidateJwtToken(token);

        if (accountId != null)
        {
            var account = await dbContext.Accounts.Where(a => a.Id == accountId.Value)
                .Include(x => x.AccountRoles)
                .ThenInclude(x => x.Role)
                .FirstOrDefaultAsync();
            // attach account to context on successful jwt validation
            context.Items["Account"] = account;
        }
        
        await _next(context);
    }
}