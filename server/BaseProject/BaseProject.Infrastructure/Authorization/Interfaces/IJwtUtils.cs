using BaseProject.Domain.Entities;

namespace BaseProject.Infrastructure.Authorization.Interfaces;

public interface IJwtUtils
{
    string GenerateJwtToken(Account account);
    Guid? ValidateJwtToken(string? token);
    RefreshToken GenerateRefreshToken(string ipAddress);
}