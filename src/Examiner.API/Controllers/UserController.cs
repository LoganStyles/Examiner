using Examiner.Application.Authentication.Interfaces;
using Examiner.Application.Users.Interfaces;
using Examiner.Domain.Dtos;
using Examiner.Domain.Dtos.Authentication;
using Examiner.Domain.Dtos.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    private const string PASSWORDS_DO_NOT_MATCH="Passwords do not match!";
    private const string ROLE_TUTOR="Tutor";

    public UserController(IAuthenticationService authenticationService, IUserService userService)
    {
        _authenticationService = authenticationService;
        _userService = userService;
    }

    /// <summary>
    /// Registers a user (Tutor or candidate)
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

        if (request.Password != request.ConfirmPassword)
            return BadRequest(PASSWORDS_DO_NOT_MATCH);

        var result = await _authenticationService.RegisterAsync(request);
        if (result.Success == true)
        {
            UserResponse userResponse = (UserResponse)result;
            return CreatedAtAction(
                nameof(GetByIdAsync),
                new { Id = userResponse.Id },
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
            return BadRequest(PASSWORDS_DO_NOT_MATCH);

        var result = await _authenticationService.ChangePasswordAsync(request);
        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    /// <summary>
    /// Fetches a user
    /// </summary>
    /// <param name="Id">The Id of the user to be fetched</param>
    /// <returns>Fetches a user or not found ActionResult</returns>
    [HttpGet("{Id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GenericResponse>> GetByIdAsync(Guid Id)
    {

        var result = await _userService.GetByIdAsync(Id);
        if (result.Success)
            return Ok(result);
        else
            return NotFound(result);
    }
    
    /// <summary>
    /// Fetches a user
    /// </summary>
    /// <param name="email">The email of the user to be fetched</param>
    /// <returns>Fetches a user or not found ActionResult</returns>
    [HttpGet("{email}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GenericResponse>> GetUserByEmailAsync(string email)
    {

        var result = await _userService.GetUserByEmail(email);
        if (result.Success)
            return Ok(result);
        else
            return NotFound(result);
    }

}