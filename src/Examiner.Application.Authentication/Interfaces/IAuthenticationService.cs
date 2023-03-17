using Examiner.Common.Dtos;
using Examiner.Domain.Dtos.Authentication;

namespace Examiner.Application.Authentication.Interfaces;

/// <summary>
/// Describes basic operations of an authentication service
/// </summary>
public interface IAuthenticationService{
    Task<GenericResponse> RegisterAsync(RegisterUserRequest request);
    Task<GenericResponse> Authenticate(AuthenticationRequest request);
    Task<GenericResponse> ChangePasswordAsync(ChangePasswordRequest request);
}