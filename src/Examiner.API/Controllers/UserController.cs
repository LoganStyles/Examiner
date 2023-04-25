using Examiner.Application.Authentication.Interfaces;
using Examiner.Application.Users.Interfaces;
using Examiner.Domain.Dtos;
using Examiner.Domain.Dtos.Authentication;
using Examiner.Domain.Dtos.Users;
using Examiner.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Examiner.Authentication.Domain.Mappings;

namespace Examiner.API.Controllers;

/// <summary>
/// Provides endpoints for authenticating a user and fetching a user's basic details
/// </summary>

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserService _userService;
    private readonly ICodeService _codeService;


    public UserController(IAuthenticationService authenticationService, IUserService userService, ICodeService codeService)
    {
        _authenticationService = authenticationService;
        _userService = userService;
        _codeService = codeService;
    }

    /// <summary>
    /// Registers a user (without a role)
    /// </summary>
    /// <param name="request">An object holding registration request data</param>
    /// <returns>Redirects to the registered user or returns bad request</returns>
    [HttpPost("signup")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GenericResponse>> RegisterAsync(
        [FromBody] RegisterUserRequest request
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = GenericResponse.Result(false, $"{AppMessages.REGISTRATION} {AppMessages.FAILED}");

        if (request.Password != request.ConfirmPassword)
        {
            response.ResultMessage = AppMessages.PASSWORDS_DO_NOT_MATCH;
            return BadRequest(response);
        }

        var result = await _authenticationService.RegisterAsync(request);
        if (result.Success == true)
        {
            UserResponse userResponse = (UserResponse)result;
            return CreatedAtAction(
                nameof(GetUserByEmailAsync),
                new { Email = userResponse.Email },
                userResponse
            );
        }
        else
        {
            return BadRequest(result);
        }

    }

    /// <summary>
    /// Authenticates a user
    /// </summary>
    /// <param name="request">An object holding authentication request data</param>
    /// <returns>A generic Response indicating success or failure of the authentication request</returns>
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GenericResponse>> LoginAsync([FromBody] AuthenticationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _authenticationService.Authenticate(request);
        if (result.Success)
            return Ok(result);
        else
            return Unauthorized(result);
    }

    /// <summary>
    /// Changes or resets a user's password. it requires authentication.
    /// </summary>
    /// <param name="request">An object holding change password request data</param>
    /// <returns>A generic Response indicating success or failure of the change password request</returns>
    [HttpPut("resetPassword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GenericResponse>> ChangePasswordAsync([FromBody] ChangePasswordRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (request.NewPassword != request.ConfirmNewPassword)
            return BadRequest(AppMessages.PASSWORDS_DO_NOT_MATCH);

        var result = await _authenticationService.ChangePasswordAsync(request);
        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    /// <summary>
    /// sets a user's role. it requires authentication.
    /// </summary>
    /// <param name="request">An object holding role request data</param>
    /// <returns>A generic Response indicating success or failure of the role request change</returns>
    [HttpPut("selectRole")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GenericResponse>> SelectRoleAsync([FromBody] SelectRoleRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authenticationService.SelectRoleAsync(request);
        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    /// <summary>
    /// verifies a previously sent to a user's email.
    /// </summary>
    /// <param name="request">An object holding code verification request data</param>
    /// <returns>A generic Response indicating success or failure of the role request change</returns>
    [HttpPut("verifyCode")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GenericResponse>> VerifyCodeAsync([FromBody] CodeVerificationRequest request)
    {

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingUser = await _userService.GetUserByEmail(request.Email);
        if (existingUser is null)
            return NotFound(GenericResponse.Result(false, $"{AppMessages.USER} {AppMessages.NOT_EXIST}"));

        var result = await _codeService.VerifyCode(existingUser, request.Code);
        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    /// <summary>
    /// verifies a code. it requires authentication.
    /// </summary>
    /// <param name="request">An object holding role request data</param>
    /// <returns>A generic Response indicating success or failure of the role request change</returns>
    [HttpPost("resendCode")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GenericResponse>> ResendVerificationCodeAsync([FromBody] ResendVerificationCodeRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingUser = await _userService.GetUserByEmail(request.Email);
        if (existingUser is null)
            return NotFound($"{AppMessages.USER} {AppMessages.NOT_EXIST}");

        var result = await _authenticationService.ResendVerificationCodeAsync(existingUser);
        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    // /// <summary>
    // /// Fetches a user
    // /// </summary>
    // /// <param name="Id">The Id of the user to be fetched</param>
    // /// <returns>Fetches a user or not found ActionResult</returns>
    // [HttpGet("{Id:Guid}")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public async Task<ActionResult<GenericResponse>> GetByIdAsync(Guid Id)
    // {

    //     var result = await _userService.GetByIdAsync(Id);
    //     if (result.Success)
    //         return Ok(result);
    //     else
    //         return NotFound(result);
    // }

    /// <summary>
    /// Fetches a user
    /// </summary>
    /// <param name="email">The email of the user to be fetched</param>
    /// <returns>Fetches a user or not found ActionResult</returns>
    [HttpGet("{email}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserResponse>> GetUserByEmailAsync(string email)
    {

        var user = await _userService.GetUserByEmail(email);
        if (user is not null)
            return Ok(ObjectMapper.Mapper.Map<UserResponse>(user));
        else
            return NotFound($"{AppMessages.USER} {AppMessages.NOT_EXIST}");
    }

}