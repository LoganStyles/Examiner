
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Examiner.Domain.Entities.Authentication;
using Examiner.Domain.Models;

namespace Examiner.Domain.Entities.Users;

/// <summary>
/// Specifies a user's basic details - A Tutor or Exam Candidate
/// </summary>
public class UserIdentity : BaseEntity
{
    [Required]
    [MaxLength(30)]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [JsonIgnore]
    public DateTime? LastEmailVerification { get; set; }

    [JsonIgnore]
    public bool IsEmailVerified => LastEmailVerification.HasValue;

    [JsonIgnore]
    [Required]
    public string PasswordHash { get; set; } = null!;

    public Role? Role { get; set; }

    public bool IsActive { get; set; }

    public CodeVerification? CodeVerification { get; set; }
    public UserProfile? UserProfile { get; set; }

}