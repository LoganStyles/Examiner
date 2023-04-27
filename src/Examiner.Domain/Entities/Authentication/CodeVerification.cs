using System.ComponentModel.DataAnnotations;
using Examiner.Domain.Entities.Users;

namespace Examiner.Domain.Entities.Authentication;

/// <summary>
/// Specifies a user's code verification operation
/// </summary>
public class CodeVerification : BaseEntity
{

    [Required]
    [MaxLength(6)]
    public string Code { get; set; } = null!;

    public Guid UserId { get; set; }
    public UserIdentity User { get; set; } = null!;

    public bool IsSent { get; set; }
    public int Attempts { get; set; }
    public int ExpiresIn { get; set; }
    public bool Expired { get; set; }

    public bool HasVerified { get; set; }
    public DateTime? VerifiedAt { get; set; }

    // public ICollection<CodeVerificationHistory>? CodeVerificationHistories { get; set; }

}