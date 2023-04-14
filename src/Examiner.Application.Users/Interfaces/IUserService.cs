using Examiner.Domain.Dtos;
using Examiner.Domain.Entities.Users;

namespace Examiner.Application.Users.Interfaces;

/// <summary>
/// Describes contract for fetching user details
/// </summary>
public interface IUserService
{
    Task<GenericResponse> GetByIdAsync(Guid Id);
    Task<User?> GetUserByEmail(string email);
    // Task<GenericResponse> GetUserByEmail(string email);
    // Task<GenericResponse> Get(
    //     string requiredStatus = "",
    //     string? requiredOrder = null,
    //     string includeProperties = "",
    //     int? take = null,
    //     int? skip = null);
}