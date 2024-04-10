using BaseProject.Domain.Entities.Abstractions.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Domain.Entities.Abstractions;

public abstract class EntityBase : IEntityBase
{
    [Key] 
    public Guid Id { get; set; }
}