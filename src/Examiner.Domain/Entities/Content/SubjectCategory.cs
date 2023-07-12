using System.ComponentModel.DataAnnotations;

namespace Examiner.Domain.Entities.Content;

public class SubjectCategory
{
    public int Id { get; set; }

    [StringLength(40)]
    [Required]
    public string? Title { get; set; }
    public ICollection<Subject>? Subjects { get; set; }
}