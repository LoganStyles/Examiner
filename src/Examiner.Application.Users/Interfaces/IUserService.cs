using Examiner.Domain.Dtos;
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
}