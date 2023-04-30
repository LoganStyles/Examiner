using Examiner.Application.Users.Interfaces;
using Examiner.Common;
using Examiner.Domain.Dtos;
using Examiner.Domain.Dtos.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Examiner.API.Controllers;

/// <summary>
/// Provides endpoints for updating a user's profile
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserProfileController : ControllerBase
{

    private readonly IUserService _userService;

    public UserProfileController(IUserService userService)
    {
        _userService = userService;
    }


    // /// <summary>
    // /// sets or updates a user's mobile phone. it requires authentication.
    // /// </summary>
    // /// <param name="request">An object holding phone update request data</param>
    // /// <returns>A generic Response indicating success or failure of the phone update request change</returns>
    // [HttpPut("phoneUpdate")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public async Task<ActionResult<GenericResponse>> PhoneUpdateAsync([FromBody] PhoneUpdateRequest request)
    // {

    //     var existingUser = await _userService.GetUserByEmail(request.Email);
    //     if (existingUser is null)
    //         return NotFound(GenericResponse.Result(false, $"{AppMessages.USER} {AppMessages.NOT_EXIST}"));

    //     if (!existingUser.IsActive)
    //         return BadRequest(GenericResponse.Result(false, $"{AppMessages.EMAIL} {AppMessages.NOT_VERIFIED}"));

    //     if (existingUser.Role is null)
    //         return BadRequest(GenericResponse.Result(false, $"{AppMessages.USER} {AppMessages.HAS_NO_ROLE}"));

    //     var result = await _userService.PhoneUpdateAsync(request, existingUser.Id);
    //     if (!result.Success)
    //         return NotFound(result);

    //     return Ok(result);
    // }

    /// <summary>
    /// sets or updates a user's profile. it requires authentication.
    /// </summary>
    /// <param name="request">An object holding profile update request data</param>
    /// <returns>A generic Response indicating success or failure of the profile update request change</returns>
    [HttpPut("profileUpdate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GenericResponse>> ProfileUpdateAsync([FromBody] ProfileUpdateRequest request)
    {

        var existingUser = await _userService.GetUserByEmail(request.Email);
        if (existingUser is null)
            return NotFound(GenericResponse.Result(false, $"{AppMessages.USER} {AppMessages.NOT_EXIST}"));

        if (!existingUser.IsActive)
            return BadRequest(GenericResponse.Result(false, $"{AppMessages.EMAIL} {AppMessages.NOT_VERIFIED}"));

        if (existingUser.Role is null)
            return BadRequest(GenericResponse.Result(false, $"{AppMessages.USER} {AppMessages.HAS_NO_ROLE}"));

        var result = await _userService.ProfileUpdateAsync(request, existingUser.Id);
        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }
}