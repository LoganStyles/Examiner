using Examiner.Domain.Dtos;
using Examiner.Domain.Dtos.Content;
using Examiner.Domain.Dtos.Users;
using Examiner.Domain.Entities.Users;

namespace Examiner.Application.Users.Interfaces;

/// <summary>
/// Describes contract for fetching user details
/// </summary>
public interface IUserService
{
    Task<UserIdentity?> GetByIdAsync(Guid Id);
    Task<UserIdentity?> GetUserByEmail(string email);
    // Task<GenericResponse> PhoneUpdateAsync(PhoneUpdateRequest request, Guid userId);
    Task<GenericResponse> ProfileUpdateAsync(ProfileUpdateRequest request, Guid userId);
    Task<GenericResponse> ProfilePhotoUpdateAsync(string filePath, Guid userId);
}