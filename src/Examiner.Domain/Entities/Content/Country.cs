namespace Examiner.Domain.Entities.Content;

public class Country : BaseEntity
{
    public new int Id { get; set; }
    public string? Code { get; set; }
    public string? Title { get; set; }
    public ICollection<State>? States { get; set; }
}