using System.ComponentModel.DataAnnotations;

namespace Examiner.Domain.Dtos.Authentication;

public record ChangePasswordRequest(

    [Required]
    [EmailAddress]
    string Email,

    [Required]
    [MinLength(6)]
    string OldPassword,

    [Required]
    [MinLength(6)]
    string NewPassword,

    [Required]
    [MinLength(6)]
    string ConfirmNewPassword

);