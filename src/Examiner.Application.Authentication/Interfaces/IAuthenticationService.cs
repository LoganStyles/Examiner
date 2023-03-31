using Examiner.Domain.Dtos;
using Examiner.Domain.Dtos.Authentication;

namespace Examiner.Application.Authentication.Interfaces;

/// <summary>
/// Contract for registering, authenticating, & password reset
/// </summary>
public interface IAuthenticationService{
    Task<GenericResponse> RegisterAsync(RegisterUserRequest request);
    Task<GenericResponse> Authenticate(AuthenticationRequest request);
    Task<GenericResponse> ChangePasswordAsync(ChangePasswordRequest request);
}