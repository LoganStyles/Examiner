namespace Examiner.Domain.Interfaces;

/// <summary>
/// Describes contract for an entity
/// </summary>
public interface IEntity
{
    Guid Id { get; set; }
}