using System.ComponentModel.DataAnnotations;

namespace Examiner.Domain.Dtos.Users;

/// <summary>
/// Specifies an email whose profile you want to fetch
/// </summary>
public record FetchProfileRequest(
    [Required]
    [EmailAddress]
    string Email
);