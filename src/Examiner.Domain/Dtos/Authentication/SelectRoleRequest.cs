using System.ComponentModel.DataAnnotations;

namespace Examiner.Domain.Dtos.Authentication;

public record SelectRoleRequest(

    [Required]
    [EmailAddress]
    string Email,

    [Required]
    [StringLength(20,MinimumLength =5,ErrorMessage ="The {0} value must be between {1} and {2} characters" )]
    string Role
);