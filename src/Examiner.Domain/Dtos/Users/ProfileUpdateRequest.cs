using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Examiner.Domain.Dtos.Users;

/// <summary>
/// Specifies an object for profile update request
/// </summary>
public record ProfileUpdateRequest(

    [Required]
    [EmailAddress]
    string Email,

    [Required]
    [StringLength(20)]
    string FirstName,

    [Required]
    [StringLength(20)]
    string LastName,

    [Required]
    [StringLength(10)]
    string MobilePhone,

    DateTime DateOfBirth,

    [Required]
    int CountryId,

    [Required]
    int StateId,
    
    [Required]
    int SubjectCategoryId,
    
    [Required]
    int ExperienceLevelId,
    
    [Required]
    int EducationDegreeId,
    
    bool IsAvailable,
    
    [Required]
    string ShortDescription,

    IFormFile? profilePhoto,
    IFormFile? degreeCertificate

);