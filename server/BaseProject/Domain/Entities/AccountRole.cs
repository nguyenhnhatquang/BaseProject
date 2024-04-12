using Domain.Entities.Abstractions;
using Domain.Entities.Accounts;

namespace Domain.Entities;

public class AccountRole : EntityBase
{
    public required Account Account { get; init; }
    public required Role Role { get; init; }
}