using System.Text.Json.Serialization;
using BaseProject.Domain.Entities;

namespace BaseProject.DTOs.Account.Responses;

public class AccountResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string? DisplayName { get; set; }
    public string? Email { get; set; }
    public string? Avatar { get; set; }
    public Gender Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? Birthday { get; set; }
    public string JwtToken { get; set; }
    public ICollection<Role> Roles { get; set; }
    [JsonIgnore]
    public string RefreshToken { get; set; }
}