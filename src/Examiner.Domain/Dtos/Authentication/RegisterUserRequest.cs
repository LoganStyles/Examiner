using System.ComponentModel.DataAnnotations;

namespace Examiner.Domain.Dtos.Authentication;

public record RegisterUserRequest(
    [Required]
    [EmailAddress]
    string Email,

    [Required]
    [MinLength(6)]
    string Password,

    [Required]
    [MinLength(6)]
    string ConfirmPassword,

    [Required]
    [StringLength(20,MinimumLength =5,ErrorMessage ="The {0} value must be between {1} and {2} characters" )]
    string Role
);