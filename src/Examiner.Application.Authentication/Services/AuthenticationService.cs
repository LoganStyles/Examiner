using System.Text;
using Examiner.Application.Authentication.Interfaces;
using Examiner.Application.Notifications.Interfaces;
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

    private const string REGISTRATION = "Registeration ";
    private const string AUTHENTICATION = "Authentication ";
    private const string CHANGE_PASSWORD = "Change password ";
    private const string ROLE = "Role select ";
    private const string CODE_VERIFICATION_MESSAGE_SUBJECT = "Code Verification";
    private const string SUCCESSFUL = "successful ";
    private const string FAILED = "failed: ";
    private const string VERIFICATION_CODE_SENT_SUCCESS = "verification code sent successfully";
    private const string EMAIL_EXISTS = "The Email already exists";
    private const string USER_NOT_FOUND = "User not found!";
    private const string USER_NOT_ACTIVE = "User is not active!";
    private const string USER_HAS_NO_ROLE = "User has no role";
    private const string USER_ACCOUNT_NOT_VERIFIED = "User has not verified account";
    private const string INVALID_EMAIL_PASSWORD = "Invalid email or password";
    private const string INVALID_PASSWORD = "Invalid password";
    private const string INVALID_REQUEST = "Invalid request";
    private const string UNABLE_TO_AUTHENTICATE_USER = "Unable to authenticate user";
    private const string UNABLE_TO_GENERATE_TOKEN = "Unable to generate token for user";

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

            var userList = await _unitOfWork.UserRepository.Get(u => u.Email.Equals(request.Email), null, "", null, null);

            var userFound = userList.FirstOrDefault();
            if (userFound is null)
                return GenericResponse.Result(false, AUTHENTICATION + FAILED + USER_NOT_FOUND);

            if (!BC.Verify(request.Password, userFound.PasswordHash))
                return GenericResponse.Result(false, AUTHENTICATION + FAILED + INVALID_EMAIL_PASSWORD);

            if (userFound.Role is null)
                return GenericResponse.Result(false, AUTHENTICATION + FAILED + USER_HAS_NO_ROLE);

            // check if user has verified email
            if (!userFound.IsActive)
                return GenericResponse.Result(false, AUTHENTICATION + FAILED + USER_ACCOUNT_NOT_VERIFIED);


            #region get token
            var authenticationResponse = _jwtTokenHandler.GenerateJwtToken(request);
            if (authenticationResponse is null)
                return GenericResponse.Result(false, AUTHENTICATION + FAILED + UNABLE_TO_GENERATE_TOKEN);
            else
            {
                // update the response with the user's role
                authenticationResponse.Role = userFound.Role.ToString();
                return authenticationResponse;
            }
            #endregion

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
        try
        {
            var userList = await _unitOfWork.UserRepository.Get(u => u.Email.Equals(request.Email), null, "", null, null);

            var userFound = userList.FirstOrDefault();
            if (userFound is null)
                return GenericResponse.Result(false, CHANGE_PASSWORD + FAILED + USER_NOT_FOUND);

            if (!BC.Verify(request.OldPassword, userFound.PasswordHash))
                return GenericResponse.Result(false, CHANGE_PASSWORD + FAILED + INVALID_EMAIL_PASSWORD);

            // change of password has been authorized
            userFound.PasswordHash = BC.HashPassword(request.NewPassword);
            userFound.LastModified = DateTime.Now;

            await _unitOfWork.UserRepository.Update(userFound);
            await _unitOfWork.CompleteAsync();

            var response = ObjectMapper.Mapper.Map<UserResponse>(userFound);
            response.Success = true;
            response.ResultMessage = CHANGE_PASSWORD + SUCCESSFUL;
            return response;

        }
        catch (Exception ex)
        {
            _logger.LogError("Error changing password - ", ex.Message);
            return GenericResponse.Result(false, ex.Message);
        }
    }

    /// <summary>
    /// Set a user's role
    /// </summary>
    /// <param name="request">An object holding email & role request data</param>
    /// <returns>An object holding data indicating the success or failure of a user's authentication</returns>
    public async Task<GenericResponse> SelectRole(SelectRoleRequest request)
    {
        try
        {
            Role userRole;
            if (Enum.TryParse(request.Role, out userRole))
            {
                var userList = await _unitOfWork.UserRepository.Get(u => u.Email.Equals(request.Email), null, "", null, null);

                var userFound = userList.FirstOrDefault();
                if (userFound is null)
                    return GenericResponse.Result(false, ROLE + FAILED + USER_NOT_FOUND);

                userFound.Role = userRole;

                await _unitOfWork.UserRepository.Update(userFound);
                await _unitOfWork.CompleteAsync();

                var response = ObjectMapper.Mapper.Map<UserResponse>(userFound);
                response.Success = true;
                response.ResultMessage = ROLE + SUCCESSFUL;
                return response;
            }
            else
            {
                return GenericResponse.Result(false, ROLE + INVALID_REQUEST);
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
                return GenericResponse.Result(false, REGISTRATION + FAILED + INVALID_PASSWORD);

            var newUser = ObjectMapper.Mapper.Map<User>(request);
            if (newUser is null)
                return GenericResponse.Result(false, REGISTRATION + FAILED + INVALID_REQUEST);

            // does this email already exist?, if yes prevent registration
            var existingUserList = await _unitOfWork.UserRepository.Get(u => u.Email.Equals(newUser.Email), null, "", null, null);
            if (existingUserList.Count() > 0)
                return GenericResponse.Result(false, REGISTRATION + FAILED + EMAIL_EXISTS);

            // fetch a code for this new user
            var codeGenerationResponse = await _codeService.GetCode();
            if (!codeGenerationResponse.Success || codeGenerationResponse.Code is null)
                return GenericResponse.Result(false, REGISTRATION + FAILED + codeGenerationResponse.ResultMessage);

            // send the code with message service
            string message = $"Your verification code is <b>{codeGenerationResponse.Code}</b>";
            var codeSendingResponse = await _emailService.SendMessage("", newUser.Email, CODE_VERIFICATION_MESSAGE_SUBJECT, message);
            if (!codeSendingResponse.Success)
                return GenericResponse.Result(false, REGISTRATION + FAILED + codeSendingResponse.ResultMessage);
            else
            {
                var codeVerification = new CodeVerification()
                {
                    Code = codeGenerationResponse.Code,
                    UserId = newUser.Id,
                    IsSent = true
                };
                // save user & code only if we were able to send code 
                await _unitOfWork.CodeVerificationRepository.AddAsync(codeVerification);
                await _unitOfWork.UserRepository.AddAsync(newUser);
                await _unitOfWork.CompleteAsync();

                var response = ObjectMapper.Mapper.Map<UserResponse>(newUser);
                response.Success = true;
                response.ResultMessage = REGISTRATION + SUCCESSFUL + VERIFICATION_CODE_SENT_SUCCESS;
                return response;
            }

        }
        catch (Exception ex)
        {
            // fetch inner exceptions if exist
            _logger.LogError("Error registering user - ", ex.Message);
            return GenericResponse.Result(false, REGISTRATION + FAILED + ex.Message);
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