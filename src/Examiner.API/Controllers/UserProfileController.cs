using Examiner.Application.Users.Interfaces;
using Examiner.Common;
using Examiner.Domain.Dtos;
using Examiner.Domain.Dtos.Content;
using Examiner.Domain.Dtos.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Examiner.API.Controllers;

/// <summary>
/// Provides endpoints for updating a user's profile
/// </summary>
[ApiController]
[Route("api/[controller]")]
// [Authorize]
public class UserProfileController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IWebHostEnvironment _environment;

    public UserProfileController(IUserService userService, IWebHostEnvironment environment)
    {
        _userService = userService;
        _environment = environment;
    }

    /// <summary>
    /// fetches a user's profile. it requires authentication.
    /// </summary>
    /// <param name="request">the user's email address</param>
    /// <returns>A UserProfileResponse indicating success or failure of the request as well as existing profile data</returns>
    [HttpPost("get-user-profile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserProfileResponse>> GetProfileAsync(
        [FromBody] FetchProfileRequest request
    )
    {
        var existingUser = await _userService.GetUserByEmail(request.Email);
        if (existingUser is null)
            return NotFound(
                UserProfileResponse.Result(false, $"{AppMessages.USER} {AppMessages.NOT_EXIST}")
            );

        if (!existingUser.IsActive)
            return BadRequest(
                UserProfileResponse.Result(false, $"{AppMessages.EMAIL} {AppMessages.NOT_VERIFIED}")
            );

        if (existingUser.Role is null)
            return BadRequest(
                UserProfileResponse.Result(false, $"{AppMessages.USER} {AppMessages.HAS_NO_ROLE}")
            );

        var result = await _userService.GetProfileAsync(existingUser.Id);
        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    /// <summary>
    /// sets or updates a user's profile. it requires authentication.
    /// </summary>
    /// <param name="request">An object holding profile update request data</param>
    /// <returns>A generic Response indicating success or failure of the profile update request change</returns>
    [HttpPost("update-user-profile")]
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

        var profilePhotoPath = string.Empty;
        if (request.profilePhoto is not null && request.profilePhoto.Length > 0)
        {
            if (!Directory.Exists(_environment.WebRootPath + "\\uploads\\profile-photo\\"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "\\uploads\\profile-photo\\");
            }
            profilePhotoPath = _environment.WebRootPath + "\\uploads\\" + request.profilePhoto.FileName;
            using (FileStream fileStream = System.IO.File.Create(profilePhotoPath))
            {
                request.profilePhoto.CopyTo(fileStream);
                fileStream.Flush();
            }
        }

        var degreeCertificatePath = string.Empty;
        if (request.degreeCertificate is not null && request.degreeCertificate.Length > 0)
        {
            if (!Directory.Exists(_environment.WebRootPath + "\\uploads\\degree-certificate\\"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "\\uploads\\degree-certificate\\");
            }
            degreeCertificatePath = _environment.WebRootPath + "\\uploads\\" + request.degreeCertificate.FileName;
            using (FileStream fileStream = System.IO.File.Create(degreeCertificatePath))
            {
                request.degreeCertificate.CopyTo(fileStream);
                fileStream.Flush();
            }
        }

        var result = await _userService.ProfileUpdateAsync(existingUser.Id,request,profilePhotoPath,degreeCertificatePath);
        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    // [HttpPost("profilePhoto")]
    // public async Task<ActionResult<GenericResponse>> ProfilePhoto([FromForm] ProfilePhotoRequest request)
    // {
    //     var existingUser = await _userService.GetUserByEmail(request.Email);
    //     if (existingUser is null)
    //         return NotFound(GenericResponse.Result(false, $"{AppMessages.USER} {AppMessages.NOT_EXIST}"));

    //     if (!existingUser.IsActive)
    //         return BadRequest(GenericResponse.Result(false, $"{AppMessages.EMAIL} {AppMessages.NOT_VERIFIED}"));

    //     if (existingUser.Role is null)
    //         return BadRequest(GenericResponse.Result(false, $"{AppMessages.USER} {AppMessages.HAS_NO_ROLE}"));

    //     if (request.profilePhoto.Length > 0)
    //     {
    //         if (!Directory.Exists(_environment.WebRootPath + "\\uploads\\"))
    //         {
    //             Directory.CreateDirectory(_environment.WebRootPath + "\\uploads\\");
    //         }
    //         var filePath = _environment.WebRootPath + "\\uploads\\" + request.profilePhoto.FileName;
    //         using (FileStream fileStream = System.IO.File.Create(filePath))
    //         {
    //             request.profilePhoto.CopyTo(fileStream);
    //             fileStream.Flush();
    //             var result = await _userService.ProfilePhotoUpdateAsync(filePath, existingUser.Id);
    //             if (!result.Success)
    //                 return BadRequest(result);

    //             return Ok(result);
    //         }
    //     }
    //     else
    //     {
    //         return BadRequest();
    //     }
    // }
}
