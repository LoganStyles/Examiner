using Examiner.Authentication.Domain.Dtos;
using Examiner.Common.Dtos;

namespace Examiner.Authentication.Application.Interfaces;

/// <summary>
/// Describes basic operations of an authentication service
/// </summary>
public interface IAuthenticationService{
    Task<GenericResponse> RegisterAsync(RegisterUserRequest request);
    Task<GenericResponse> Authenticate(AuthenticationRequest request);
    Task<GenericResponse> ChangePasswordAsync(ChangePasswordRequest request);
}