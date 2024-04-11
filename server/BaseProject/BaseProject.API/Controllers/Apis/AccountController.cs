using BaseProject.API.Utils.Filters;
using BaseProject.DTOs.Account.Responses;
using BaseProject.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.API.Controllers.Apis;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    [AllowAnonymous]
    [HttpGet("login")]
    public async Task<ActionResult<AccountResponse>> Login(string username, string password)
    {
        var result = await _accountService.Login(username, password, GetIpAddress() ?? $"127.0.0.1/{username}");
        
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    
    private string? GetIpAddress()
    {
        if (Request.Headers.TryGetValue("X-Forwarded-For", out Microsoft.Extensions.Primitives.StringValues value))
        {
            return value;
        }

        return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
    }
}