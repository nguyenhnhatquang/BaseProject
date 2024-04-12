namespace Domain.Entities.Abstractions.Interfaces;

public interface IDateTrackable
{
    DateTime CreatedOnUtc { get; set; }
    DateTime? ModifiedOnUtc { get; set; }
}