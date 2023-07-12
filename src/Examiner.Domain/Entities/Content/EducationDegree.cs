using System.ComponentModel.DataAnnotations;

namespace Examiner.Domain.Entities.Content;

public class EducationDegree : BaseEntity
{
    public new int Id { get; set; }
    
    [StringLength(20)]
    [Required]
    public string? Title { get; set; }
}