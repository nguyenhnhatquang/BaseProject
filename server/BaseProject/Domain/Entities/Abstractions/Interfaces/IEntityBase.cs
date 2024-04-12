namespace Domain.Entities.Abstractions.Interfaces;

public interface IEntityBase
{
    Guid Id { get; set; }
    
    IReadOnlyList<IDomainEvent> GetDomainEvents();

    void ClearDomainEvents();
}