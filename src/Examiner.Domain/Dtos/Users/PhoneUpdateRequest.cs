using System.ComponentModel.DataAnnotations;

namespace Examiner.Domain.Dtos.Users;

/// <summary>
/// Specifies an object for phone update request
/// </summary>
public record PhoneUpdateRequest(

    [Required]
    [EmailAddress]
    string Email,

    [Required]
    [StringLength(3)]
    string CountryCode,

    [Required]
    [StringLength(10)]
    string MobilePhone
);