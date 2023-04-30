using System.ComponentModel.DataAnnotations;

namespace Examiner.Domain.Dtos.Users;

/// <summary>
/// Specifies an object for profile update request
/// </summary>
public record ProfileUpdateRequest(

    [Required]
    [EmailAddress]
    string Email,

    [Required]
    [StringLength(3)]
    string CountryCode,

    [Required]
    [StringLength(10)]
    string MobilePhone,

    [Required]
    [StringLength(20)]
    string FirstName,

    [Required]
    [StringLength(20)]
    string LastName,

    [Required]
    DateOnly DateOfBirth,

    [Required]
    HashSet<int> SubjectIds,

    [Required]
    [StringLength(50)]
    string Location

);