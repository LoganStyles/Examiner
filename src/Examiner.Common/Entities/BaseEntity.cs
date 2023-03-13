using System.ComponentModel.DataAnnotations;
using Examiner.Common.Interfaces;

namespace Examiner.Common.Entities;

/// <summary>
/// Implements the basic properties for an entity
/// </summary>
public class BaseEntity : IEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}