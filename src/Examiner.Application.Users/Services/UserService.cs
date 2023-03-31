using System.Linq.Expressions;
using Examiner.Domain.Dtos.Users;
using Examiner.Domain.Entities.Users;
using Examiner.Infrastructure.UnitOfWork.Interfaces;
using Microsoft.Extensions.Logging;
using Examiner.Authentication.Domain.Mappings;
using Examiner.Application.Users.Interfaces;
using Examiner.Domain.Dtos;

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
    public async Task<GenericResponse> GetUserByEmail(string email)
    {
        try
        {
            Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null;
            Expression<Func<User, bool>>? filter = (u => u.Email == email);

            var users = await _unitOfWork.UserRepository.Get(filter, orderBy, string.Empty, null, null);
            if (users.Count() > 0)
            {
                var response = ObjectMapper.Mapper.Map<UserResponse>(users.FirstOrDefault());
                response.Success = true;
                response.ResultMessage = "Fetching user was successful";
                return response;
            }
            return GenericResponse.Result(false, "No user found");

        }
        catch (Exception ex)
        {
            _logger.LogError("Error fetching user - ", ex.Message);
            return GenericResponse.Result(false, ex.Message);
        }
    }

    /// <summary>
    /// Fetches a user by Id
    /// </summary>
    /// <param name="Id">An Id representing the user to be fetched</param>
    /// <returns>An object holding data indicating the success or failure of fetching the user</returns>
    public async Task<GenericResponse> GetByIdAsync(Guid Id)
    {
        try
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(Id);
            if (user is not null)
            {
                var response = ObjectMapper.Mapper.Map<UserResponse>(user);
                response.Success = true;
                response.ResultMessage = "Fetching user was successful";
                return response;
            }
            return GenericResponse.Result(false, "Fetching user failed");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error fetching user - ", ex.Message);
            return GenericResponse.Result(false, ex.Message);
        }
    }
}