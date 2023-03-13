using System.Text;
using Examiner.Authentication.Application.Interfaces;
using Examiner.Authentication.Domain;
using Examiner.Authentication.Domain.Dtos;
using Examiner.Authentication.Domain.Entities;
using Examiner.Authentication.Domain.Mappings;
using Examiner.Common.Dtos;
using Examiner.Infrastructure.UnitOfWork.Interfaces;
using Microsoft.Extensions.Logging;
using BC = BCrypt.Net.BCrypt;

namespace Examiner.Authentication.Application.Services;

public class AuthenticationService : IAuthenticationService
{

    private readonly IJwtTokenHandler _jwtTokenHandler;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AuthenticationService> _logger;

    public AuthenticationService(
        IJwtTokenHandler jwtTokenHandler,
        IUnitOfWork unitOfWork,
        ILogger<AuthenticationService> logger)
    {
        _jwtTokenHandler = jwtTokenHandler;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <summary>
    /// Authenticates a user
    /// </summary>
    /// <param name="request">An object holding authentication request data</param>
    /// <returns>An object holding data indicating the success or failure of a user's authentication</returns>
    public async Task<GenericResponse> Authenticate(AuthenticationRequest request)
    {
        try
        {

            var userList = await _unitOfWork.UserRepository.Get(
                u => u.Email.Equals(request.Email), null, "", null, null);

            var userFound = userList.FirstOrDefault();
            if (userFound is not null)
            {
                if (!userFound.IsActive)
                {
                    return GenericResponse.Result(false, "Authentication failed: User is disabled!");
                }

                if (!BC.Verify(request.Password, userFound.PasswordHash))
                {
                    return GenericResponse.Result(false, "Authentication failed: Invalid Email or password!");
                }

                #region get token
                var authenticationResponse = _jwtTokenHandler.GenerateJwtToken(request);
                if (authenticationResponse is null)
                {
                    return GenericResponse.Result(false, "Authentication failed: Unable to authenticate!");
                }
                else
                {
                    return authenticationResponse;
                }
                #endregion
            }
            else
            {
                return GenericResponse.Result(false, "Authentication failed: User not found!");
            }
        }
        catch (Exception ex)
        {
            // fetch inner exceptions if exist
            _logger.LogError("Error registering user - ", ex.Message);
            return GenericResponse.Result(false, ex.Message);
        }
    }

    /// <summary>
    /// Changes a user's password
    /// </summary>
    /// <param name="request">An object holding change password request data</param>
    /// <returns>An object holding data indicating the success or failure of a user's authentication</returns>
    public async Task<GenericResponse> ChangePasswordAsync(ChangePasswordRequest request)
    {
        StringBuilder respMsg = new StringBuilder("Change password request ");
        try
        {
            var userList = await _unitOfWork.UserRepository.Get(
                u => u.Email.Equals(request.Email), null, "", null, null);

            var userFound = userList.FirstOrDefault();
            if (userFound is not null)
            {
                if (!BC.Verify(request.OldPassword, userFound.PasswordHash))
                    return GenericResponse.Result(false, respMsg.Append("failed: Invalid Email or password!").ToString());

                // if (!userFound.Id.Equals(request.AuthorizerId))
                // {
                //     //A different user is making this request so verify the role
                //     if (!await IsAuthorizedForThisUserAsync(Guid.Parse(request.AuthorizerId), userFound.Role))
                //         return GenericResponse.Result(false, respMsg.Append("failed: Invalid authorizer!").ToString());
                // }

                // change of password has been authorized
                userFound.PasswordHash = BC.HashPassword(request.NewPassword);
                userFound.LastModified = DateTime.Now;
                // if admin changes password also make account active otherwise don't change active status
                // if (userFound.Role.Equals(Role.Administrator))
                //     userFound.Active = true;

                await _unitOfWork.UserRepository.Update(userFound);
                await _unitOfWork.CompleteAsync();

                var response = ObjectMapper.Mapper.Map<UserResponse>(userFound);
                response.Success = true;
                response.ResultMessage = respMsg.Append("was successful").ToString();
                return response;
            }
            else
            {
                return GenericResponse.Result(false, respMsg.Append("failed: User not found!").ToString());
            }

        }
        catch (Exception ex)
        {
            _logger.LogError("Error changing password - ", ex.Message);
            return GenericResponse.Result(false, ex.Message);
        }
    }

    /// <summary>
    /// Registers a user
    /// </summary>
    /// <param name="request">An object holding registration request data</param>
    /// <returns>An object holding a registered user as well data  indicating the success or failure of the registration</returns>
    public async Task<GenericResponse> RegisterAsync(RegisterUserRequest request)
    {
        try
        {
            if (!IsValidPassword(request.Password))
                return GenericResponse.Result(false, "Registration failed: Invalid password provided");

            Role userRole;
            if (Enum.TryParse(request.Role, out userRole))
            {

                var newUser = ObjectMapper.Mapper.Map<User>(request);
                if (newUser is not null)
                {
                    var existingUserList = await _unitOfWork.UserRepository
                    .Get(u => u.Email.Equals(newUser.Email), null, "", null, null);
                    if (existingUserList.Count() > 0)
                        return GenericResponse.Result(false, "Registration failed: The Email already exists");

                    newUser.IsActive = true;
                    await _unitOfWork.UserRepository.AddAsync(newUser);
                    await _unitOfWork.CompleteAsync();

                    var response = ObjectMapper.Mapper.Map<UserResponse>(newUser);
                    response.Success = true;
                    response.ResultMessage = "Registering user was successful";
                    return response;
                }
                else
                {
                    return GenericResponse.Result(false,
                    "Registration failed: Invalid user request provided");
                }
            }
            else
            {
                return GenericResponse.Result(false, "Registering user failed: Invalid user role provided");
            }
        }
        catch (Exception ex)
        {
            // fetch inner exceptions if exist
            _logger.LogError("Error registering user - ", ex.Message);
            return GenericResponse.Result(false, ex.Message);
        }
    }

    private bool IsValidPassword(string password)
    {
        return password.Any(char.IsUpper)
        && password.Any(char.IsLower)
        && password.Any(p => !char.IsLetterOrDigit(p))
        && password.Any(char.IsDigit);
    }

}