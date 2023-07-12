using System.ComponentModel.DataAnnotations;

namespace Examiner.Domain.Entities.Content;

public class State : BaseEntity
{
    public new int Id { get; set; }
    [StringLength(30)]
    [Required]
    public string? Title { get; set; }
    public int CountryId { get; set; }
    public Country? Country { get; set; }
}