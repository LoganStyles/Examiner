using System.ComponentModel.DataAnnotations;
using Examiner.Domain.Interfaces;

namespace Examiner.Domain.Entities;

/// <summary>
/// Implements the basic properties for an entity
/// </summary>
public class BaseEntity : IEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}