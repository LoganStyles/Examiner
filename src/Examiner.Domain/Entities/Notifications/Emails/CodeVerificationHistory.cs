using System.ComponentModel.DataAnnotations;
using Examiner.Common.Entities;

namespace Examiner.Domain.Entities.Notifications.Emails;

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