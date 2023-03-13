using System.ComponentModel.DataAnnotations;

namespace Examiner.Authentication.Domain.Dtos;

/// <summary>
/// Specifies data for an authentication request
/// </summary>
public record AuthenticationRequest(
    [Required]
    [EmailAddress]
    string Email,

    [Required]
    [MinLength(6)]
    string Password,

    [Required]
    [StringLength(20,MinimumLength =5,ErrorMessage ="The {0} value must be between {1} and {2} characters" )]
    string Role
);