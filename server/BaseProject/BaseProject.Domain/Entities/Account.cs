using System.Text.Json.Serialization;
using BaseProject.Domain.Entities.Abstractions;

namespace BaseProject.Domain.Entities;

public enum Gender
{
    Male,
    Female,
    Other
}

public class Account :EntityAuditBase
{
    public required string Username { get; init; }
    public required string PasswordHash { get; init; }
    public string? DisplayName { get; init; }
    public string? Email { get; init; }
    public string? Avatar { get; init; }
    public required Gender Gender { get; init; }
    public string? PhoneNumber { get; init; }
    public DateTime? Birthday { get; init; }
    public required bool IsVerified { get; init; }
    public string? VerificationToken { get; init; }
    public required bool AcceptTerms { get; init; }
    [JsonIgnore]
    public ICollection<AccountRole> AccountRoles { get; init; }
    [JsonIgnore]
    public List<RefreshToken>? RefreshTokens { get; set; }

    public bool OwnsToken(string token)
    {
        return RefreshTokens?.Find(x => x.Token == token) != null;
    }
}