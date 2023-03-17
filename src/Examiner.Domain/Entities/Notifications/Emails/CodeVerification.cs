using System.ComponentModel.DataAnnotations;
using Examiner.Common.Entities;
using Examiner.Domain.Entities.Users;

namespace Examiner.Domain.Entities.Notifications.Emails;

/// <summary>
/// Specifies a user's code verification operation
/// </summary>
public class CodeVerification : BaseEntity
{

    [Required]
    [MaxLength(6)]
    public string Code { get; set; } = null!;

    public Guid UserId { get; set; }
    public User User {get; set;} = null!;

    public bool Expired { get; set; }

    public bool CanResend { get; set; }

    public bool HasVerified { get; set; }
    public DateTime? VerifiedAt { get; set; }

    public ICollection<CodeVerificationHistory>? CodeVerifications {get;set;}

}