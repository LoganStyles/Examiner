using Examiner.Domain.Dtos;
using Examiner.Domain.Dtos.Authentication;
using Examiner.Domain.Entities.Users;

namespace Examiner.Application.Authentication.Interfaces;

/// <summary>
/// Contract for registering, authenticating, & password reset
/// </summary>
public interface IAuthenticationService
{
    Task<GenericResponse> RegisterAsync(RegisterUserRequest request);
    Task<GenericResponse> Authenticate(AuthenticationRequest request);
    Task<GenericResponse> ChangePasswordAsync(ChangePasswordRequest request);
    Task<GenericResponse> SelectRoleAsync(SelectRoleRequest request);
    Task<GenericResponse> ResendVerificationCodeAsync(UserIdentity user);
}