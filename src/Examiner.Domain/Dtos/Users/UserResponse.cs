using Examiner.Domain.Dtos;

namespace Examiner.Domain.Dtos.Users;

/// <summary>
/// Implements contract for a user response object
/// </summary>
public class UserResponse : GenericResponse
{

    public UserResponse() : base(false, null)
    {

    }

    public UserResponse(bool success, string? message) : base(success, message) { }

    public Guid? Id { get; set; }
    public string? FirstName { get; set; }

    public string? LastName { get; set; }
    public string? Role { get; set; }

    public string? Email { get; set; }
    public string? MobilePhone { get; set; }
}