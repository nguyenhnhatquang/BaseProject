using System.Text.Json.Serialization;
using BaseProject.Domain.Entities.Abstractions;

namespace BaseProject.Domain.Entities;

public enum AccountPermission
{
    SuperAdmin,
    Member,
}

public class Role : EntityBase
{
    public required string Name { get; init; }
    public required AccountPermission Type { get; init; }
    [JsonIgnore]
    public ICollection<AccountRole>? AccountRoles { get; set; }
}