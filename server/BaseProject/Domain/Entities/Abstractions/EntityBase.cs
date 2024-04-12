using System.ComponentModel.DataAnnotations;
using Domain.Entities.Abstractions.Interfaces;

namespace Domain.Entities.Abstractions;

public class EntityBase : IEntityBase
{
    private readonly List<IDomainEvent> _domainEvents = new();

    [Key]
    public Guid Id { get; set; }

    protected EntityBase()
    {
    }

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.ToList();
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}