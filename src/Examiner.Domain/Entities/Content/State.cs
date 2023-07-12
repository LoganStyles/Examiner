namespace Examiner.Domain.Entities.Content;

public class State : BaseEntity
{
    public new int Id { get; set; }
    public string? Title { get; set; }
    public int CountryId { get; set; }
    public Country? Country { get; set; }
}