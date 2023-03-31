using System.Text;
using Examiner.Application.Authentication.Interfaces;
using Examiner.Application.Notifications.Interfaces;
using Examiner.Application.Notifications.Services;
using Examiner.Authentication.Domain.Mappings;
using Examiner.Domain.Dtos;
using Examiner.Domain.Dtos.Authentication;
using Examiner.Domain.Dtos.Users;
using Examiner.Domain.Entities.Authentication;
using Examiner.Domain.Entities.Users;
using Examiner.Domain.Models;
using Examiner.Infrastructure.UnitOfWork.Interfaces;
using Microsoft.Extensions.Logging;
using BC = BCrypt.Net.BCrypt;

namespace Examiner.Application.Authentication.Services;

/// <summary>
/// Implements registration, authentication, & password reset
/// </summary>
public class AuthenticationService : IAuthenticationService
{

    private readonly IJwtTokenHandler _jwtTokenHandler;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AuthenticationService> _logger;
    private readonly ICodeService _codeService;
    private readonly IEmailService _emailService;

    private const string USER_REGISTRATION_SUCCESSFUL = "Registering user was successful, and verification code sent successfully";

    public AuthenticationService(
        IJwtTokenHandler jwtTokenHandler,
        IUnitOfWork unitOfWork,
        ILogger<AuthenticationService> logger,
        ICodeService codeService,
        IEmailService emailService)
    {
        _jwtTokenHandler = jwtTokenHandler;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _codeService = codeService;
        _emailService = emailService;
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

                // change of password has been authorized
                userFound.PasswordHash = BC.HashPassword(request.NewPassword);
                userFound.LastModified = DateTime.Now;

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
                    // does this email already exist?, if yes prevent registration
                    var existingUserList = await _unitOfWork.UserRepository.Get(u => u.Email.Equals(newUser.Email), null, "", null, null);
                    if (existingUserList.Count() > 0)
                        return GenericResponse.Result(false, "Registration failed: The Email already exists");

                    // fetch a code for this new user
                    var codeGenerationResponse = await _codeService.GetCode();
                    if (!codeGenerationResponse.Success)
                        return GenericResponse.Result(false, codeGenerationResponse.ResultMessage);

                    // send the code with message service
                    var codeSendingResponse = await _emailService.SendMessage(newUser.Email, codeGenerationResponse.Code!);
                    if (!codeSendingResponse.Success)
                        return GenericResponse.Result(false, codeSendingResponse.ResultMessage);
                    else
                    {
                        var codeVerification = new CodeVerification()
                        {
                            Code = codeGenerationResponse.Code!,
                            UserId = newUser.Id,
                            IsSent = true
                        };
                        // save user & code only if we were able to send code 
                        await _unitOfWork.CodeVerificationRepository.AddAsync(codeVerification);
                        await _unitOfWork.UserRepository.AddAsync(newUser);
                        await _unitOfWork.CompleteAsync();

                        var response = ObjectMapper.Mapper.Map<UserResponse>(newUser);
                        response.Success = true;
                        response.ResultMessage = USER_REGISTRATION_SUCCESSFUL;
                        return response;
                    }
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

    /// <summary>
    /// Registers a user
    /// </summary>
    /// <param name="request">An object holding registration request data</param>
    /// <returns>An object holding a registered user as well data  indicating the success or failure of the registration</returns>
    private bool IsValidPassword(string password)
    {
        return password.Any(char.IsUpper)
        && password.Any(char.IsLower)
        && password.Any(p => !char.IsLetterOrDigit(p))
        && password.Any(char.IsDigit);
    }

}