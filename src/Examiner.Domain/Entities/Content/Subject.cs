using System.ComponentModel.DataAnnotations;
using Examiner.Domain.Entities.Users;

namespace Examiner.Domain.Entities.Content;

public class Subject
{
    public int Id { get; set; }
    [StringLength(40)]
    [Required]
    public string? Title { get; set; }
    public int SubjectCategoryId { get; set; }
    public SubjectCategory? SubjectCategory { get; set; }
    public ICollection<UserProfile>? UserProfiles { get; set; }
}