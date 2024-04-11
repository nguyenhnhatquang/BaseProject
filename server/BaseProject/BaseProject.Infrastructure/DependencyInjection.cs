using BaseProject.Infrastructure.Authorization;
using BaseProject.Infrastructure.Authorization.Interfaces;
using BaseProject.Infrastructure.Interfaces;
using BaseProject.Infrastructure.Repositories;
using BaseProject.Infrastructure.Services;
using BaseProject.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BaseProject.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IJwtUtils, JwtUtils>();
        services.AddScoped<IAccountService, AccountService>();

        return services;
    }
}