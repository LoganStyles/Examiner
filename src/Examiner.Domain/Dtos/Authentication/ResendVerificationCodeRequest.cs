using System.ComponentModel.DataAnnotations;

namespace Examiner.Domain.Dtos.Authentication;

/// <summary>
/// Specifies an email to resend code
/// </summary>
public record ResendVerificationCodeRequest(
    [Required]
    [EmailAddress]
    string Email
);