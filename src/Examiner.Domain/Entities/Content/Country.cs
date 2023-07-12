using System.ComponentModel.DataAnnotations;

namespace Examiner.Domain.Entities.Content;

public class Country : BaseEntity
{
    public new int Id { get; set; }
    [StringLength(3)]
    [Required]
    public string? Code { get; set; }
    [StringLength(30)]
    [Required]
    public string? Title { get; set; }
    public ICollection<State>? States { get; set; }
}