using System.Linq.Expressions;
using Examiner.Domain.Entities.Users;
using Examiner.Infrastructure.UnitOfWork.Interfaces;
using Microsoft.Extensions.Logging;
using Examiner.Application.Users.Interfaces;

namespace Examiner.Application.Users.Services;

/// <summary>
/// Implements contract for fetching user details
/// </summary>
public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserService> _logger;

    public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <summary>
    /// Fetches a user by email
    /// </summary>
    /// <param name="email">The email of the user to fetch</param>
    /// <returns>An object holding data indicating the success or failure of fetching the users</returns>
    public async Task<UserIdentity?> GetUserByEmail(string email)
    {
        // var response = new UserResponse(false,$"{AppMessages.USER} {AppMessages.NOT_EXIST}");
        try
        {
            Func<IQueryable<UserIdentity>, IOrderedQueryable<UserIdentity>>? orderBy = null;
            Expression<Func<UserIdentity, bool>>? filter = (u => u.Email == email);

            var users = await _unitOfWork.UserRepository.Get(filter, orderBy, "CodeVerification", null, null);
            if (users.Count() > 0)
            {
                return users.FirstOrDefault();
            }
            return null;

        }
        catch (Exception ex)
        {
            _logger.LogError("Error fetching user - ", ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Fetches a user by Id
    /// </summary>
    /// <param name="Id">An Id representing the user to be fetched</param>
    /// <returns>An object holding data indicating the success or failure of fetching the user</returns>
    public async Task<UserIdentity?> GetByIdAsync(Guid Id)
    {
        try
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(Id);
            if (user is not null)
            {
                return user;
            }
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error fetching user - ", ex.Message);
            throw;
        }
    }

}