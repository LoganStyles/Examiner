using Examiner.Domain.Dtos.Authentication;

namespace Examiner.Application.Authentication.Interfaces;

/// <summary>
/// Contract for generating a JWT token
/// </summary>
public interface IJwtTokenHandler
{
    AuthenticationResponse? GenerateJwtToken(AuthenticationRequest request);
}