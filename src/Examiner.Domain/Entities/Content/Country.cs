using System.ComponentModel.DataAnnotations;

namespace Examiner.Domain.Entities.Content;

public class Country
{
    public int Id { get; set; }
    [StringLength(3)]
    [Required]
    public string? Code { get; set; }
    [StringLength(40)]
    [Required]
    public string? Title { get; set; }
    public ICollection<State>? States { get; set; }
}