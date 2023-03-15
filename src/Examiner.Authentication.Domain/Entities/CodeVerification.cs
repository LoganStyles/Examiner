using System.ComponentModel.DataAnnotations;
using Examiner.Common.Entities;

namespace Examiner.Authentication.Domain.Entities;

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