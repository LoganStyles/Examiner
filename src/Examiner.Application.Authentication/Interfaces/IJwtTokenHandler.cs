using Examiner.Domain.Dtos.Authentication;

namespace Examiner.Application.Authentication.Interfaces;

/// <summary>
/// Generates an authentication token
/// </summary>
public interface IJwtTokenHandler
{
    AuthenticationResponse? GenerateJwtToken(AuthenticationRequest request);
}