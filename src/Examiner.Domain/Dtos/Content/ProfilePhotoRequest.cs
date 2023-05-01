using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Examiner.Domain.Dtos.Content;

/// <summary>
/// Specifies data for a profile photo request
/// </summary>
public record ProfilePhotoRequest(
    [Required]
    [EmailAddress]
    string Email,

    IFormFile profilePhoto
);