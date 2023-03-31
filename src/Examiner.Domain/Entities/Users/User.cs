
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Examiner.Domain.Entities.Authentication;
using Examiner.Domain.Models;

namespace Examiner.Domain.Entities.Users;

/// <summary>
/// Specifies a user's basic details - A Tutor or Exam Candidate
/// </summary>
public class User : BaseEntity
{
    [MaxLength(20)]
    public string? FirstName { get; set; }

    [MaxLength(20)]
    public string? LastName { get; set; }

    [Required]
    [MaxLength(30)]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [JsonIgnore]
    public string? EmailVerificationToken { get; set; }

    [JsonIgnore]
    public DateTime? LastEmailVerification { get; set; }

    [JsonIgnore]
    public bool IsEmailVerified => LastEmailVerification.HasValue;

    public string? MobilePhone { get; set; }

    [JsonIgnore]
    public DateTime? LastMobilePhoneVerification { get; set; }

    [JsonIgnore]
    public bool IsMobilePhoneVerified => LastMobilePhoneVerification.HasValue;

    [JsonIgnore]
    [Required]
    public string PasswordHash { get; set; } = null!;

    public Role Role { get; set; }

    [JsonIgnore]
    public DateTime? LastPasswordReset { get; set; }

    [JsonIgnore]
    public DateTime? LastModified { get; set; }

    public bool IsActive { get; set; }

    public CodeVerification? CodeVerification { get; set; }

}