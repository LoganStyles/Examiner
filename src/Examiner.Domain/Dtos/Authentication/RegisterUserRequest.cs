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
    string ConfirmPassword

);