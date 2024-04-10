using BaseProject.Domain.Entities.Abstractions;

namespace BaseProject.Domain.Entities;

public class RefreshToken : EntityBase
{
    public Account Account { get; init; }
    public required string Token { get; init; }
    public required string CreatedByIp { get; init; }
    public required DateTime CreatedOnUtc { get; init; }
    public required DateTime ExpiredOnUtc { get; init; }
    public DateTime? Revoked { get; init; }
    public string? RevokedByIp { get; init; }
    public string? ReplacedByToken { get; init; }
    public string? ReasonRevoked { get; init; }
    private bool IsExpired => DateTime.UtcNow >= ExpiredOnUtc;
    
    public bool IsRevoked => Revoked != null;
    public bool IsActive => Revoked == null && !IsExpired;
}