using System.ComponentModel.DataAnnotations;

namespace Examiner.Domain.Entities.Authentication;

/// <summary>
/// Specifies a user's code verification history
/// </summary>

public class CodeVerificationHistory : BaseEntity
{
    [Required]
    public Guid CodeVerificationId { get; set; }
    public CodeVerification? CodeVerification { get; set; }
    public string Message { get; set; } = null!;
}