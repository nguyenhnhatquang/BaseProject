using BaseProject.Domain.Entities.Abstractions;

namespace BaseProject.Domain.Entities;

public class AccountRole : EntityBase
{
    public required Account Account { get; init; }
    public required Role Role { get; init; }
}