namespace Examiner.Common.Interfaces;

/// <summary>
/// Describes basic properties for an entity
/// </summary>
public interface IEntity
{
    Guid Id { get; set; }
}