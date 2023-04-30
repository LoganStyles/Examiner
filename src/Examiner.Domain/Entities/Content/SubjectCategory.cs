namespace Examiner.Domain.Entities.Content;

public class SubjectCategory : BaseEntity
{
    public new int Id { get; set; }
    public string? Title { get; set; }
    public ICollection<Subject>? Subjects { get; set; }
}