using Examiner.Domain.Entities.Authentication;

namespace Examiner.Domain.Dtos.Users;

public class UserDto
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }
    public string? Role { get; set; }

    public string? Email { get; set; }
    public string? MobilePhone { get; set; }
    public CodeVerification? CodeVerification { get; set; }
}