using BaseProject.Domain.Entities.Abstractions.Interfaces;

namespace BaseProject.Domain.Entities.Abstractions;

public class EntityAuditBase : EntityBase, IDateTrackable, ISoftDeletable
{
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOnUtc { get; set; }
}