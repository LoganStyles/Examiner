using Examiner.Common.Dtos;

namespace Examiner.Domain.Dtos.Authentication;

/// <summary>
/// Specifies the result of an authentication request
/// </summary>
public class AuthenticationResponse : GenericResponse
{
    public AuthenticationResponse() : base(false, null)
    {

    }

    public AuthenticationResponse(bool success, string? message) : base(success, message)
    {

    }

    public string? Email { get; set; }
    public string? JwtToken { get; set; }
    public int ExpiresIn { get; set; }
}