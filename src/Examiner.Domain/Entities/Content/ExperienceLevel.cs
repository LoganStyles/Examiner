using System.ComponentModel.DataAnnotations;

namespace Examiner.Domain.Entities.Content;

public class ExperienceLevel
{
    public int Id { get; set; }
    
    [StringLength(40)]
    [Required]
    public string? Title { get; set; }
}