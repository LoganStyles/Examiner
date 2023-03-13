using Examiner.Authentication.Domain.Dtos;

namespace Examiner.Authentication.Application.Interfaces;

/// <summary>
/// Generates an authentication token
/// </summary>
public interface IJwtTokenHandler
{
    AuthenticationResponse? GenerateJwtToken(AuthenticationRequest request);
}