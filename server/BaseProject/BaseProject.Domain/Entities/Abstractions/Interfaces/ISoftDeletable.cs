namespace BaseProject.Domain.Entities.Abstractions.Interfaces;

public interface ISoftDeletable
{
    bool IsDeleted { get; set; }
    DateTime? DeletedOnUtc { get; set; }
}