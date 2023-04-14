using System.ComponentModel.DataAnnotations;

namespace Examiner.Domain.Dtos.Authentication;

/// <summary>
/// Specifies data for an code verification request
/// </summary>
public record CodeVerificationRequest(
    [Required]
    [EmailAddress]
    string Email,

    [Required]
    [StringLength(6)]
    string Code
);