using Examiner.Domain.Entities.Users;

namespace Examiner.Domain.Entities.Content;

public class Subject : BaseEntity
{
    public new int Id { get; set; }
    public string? Title { get; set; }
    public int SubjectCategoryId { get; set; }
    public SubjectCategory? SubjectCategory { get; set; }
    public ICollection<UserProfile>? UserProfiles { get; set; }
}