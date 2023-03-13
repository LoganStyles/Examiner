using System.Linq.Expressions;
using Examiner.Authentication.Domain.Dtos;
using Examiner.Authentication.Domain.Entities;
using Examiner.Authentication.Domain.Mappings;
using Examiner.Common.Dtos;
using Examiner.Infrastructure.UnitOfWork.Interfaces;
using Examiner.Users.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Examiner.Users.Application.Services;

/// <summary>
/// Provides user specific operations
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
    /// Fetches a collection of users
    /// </summary>
    /// <param name="requiredStatus"></param>
    /// <returns>An object holding data indicating the success or failure of fetching the users</returns>
    public Task<GenericResponse> Get(string requiredStatus = "", string? requiredOrder = null, string includeProperties = "", int? take = null, int? skip = null)
    {
        throw new NotImplementedException();
        // try
        // {
        //     Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null;

        //     //filter - active or inactive
        //     Expression<Func<User, bool>>? filter = (u => u.AuthorizerId != null);
        //     if (!String.IsNullOrWhiteSpace(requiredStatus))
        //     {
        //         UserAccountStatus status;
        //         if (Enum.TryParse(requiredStatus, out status))
        //         {
        //             if (status.Equals(UserAccountStatus.Active))
        //                 filter = (u => u.AuthorizerId != null && u.Active == true);
        //             else
        //                 filter = (u => u.AuthorizerId != null && u.Active == false);
        //         }
        //     }

        //     var users = await _unitOfWork.UserRepository.Get(filter, orderBy, includeProperties, take, skip);
        //     if (users.Count() > 0)
        //     {
        //         UsersResponse response = new();
        //         response.Users = new List<UserDto>();
        //         foreach (var user in users)
        //         {
        //             var userDto = ObjectMapper.Mapper.Map<UserDto>(users);
        //             response.Users.Add(userDto);
        //         }
        //         response.Success = true;
        //         response.ResultMessage = "Fetching users was successful";
        //         return response;
        //     }
        //     return GenericResponse.Result(true, "No users found");
        // }
        // catch (Exception ex)
        // {
        //     _logger.LogError("Error fetching user - ", ex.Message);
        //     return GenericResponse.Result(false, ex.Message);
        // }
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