using Examiner.API.Controllers;
using Examiner.Application.Users.Interfaces;
using Examiner.Common;
using Examiner.Domain.Dtos;
using Examiner.Domain.Entities.Users;
using Examiner.Tests.MockData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Examiner.Tests.Examiner.API.Controllers;

public class UserProfileControllerTests
{

    private readonly Mock<IUserService> _userService;
    private readonly UserProfileController _userProfileController;
    private readonly Mock<IWebHostEnvironment> _environment;

    public UserProfileControllerTests()
    {
        _userService = new();
        _environment = new();
        _userProfileController = new UserProfileController(_userService.Object,_environment.Object);
    }

    #region mobile phone update

    // [Fact]
    // public async Task PhoneUpdateAsync_NoneExistingUser_Returns404NotFoundResponseStatus()
    // {
    //     var request = UserMock.GetExistingUserPhoneUpdateRequest();
    //     UserIdentity? returnedUser = null;
    //     _userService.Setup(u => u.GetUserByEmail(request.Email)).ReturnsAsync(returnedUser);

    //     var result = await _userProfileController.PhoneUpdateAsync(request);

    //     var actionResult = Assert.IsType<ActionResult<GenericResponse>>(result);
    //     var notFoundObjResult = Assert.IsType<NotFoundObjectResult>(result.Result);
    //     var returnValue = Assert.IsType<GenericResponse>(notFoundObjResult.Value);
    //     Assert.False(returnValue.Success);
    //     Assert.Contains($"{AppMessages.USER} {AppMessages.NOT_EXIST}", returnValue.ResultMessage);
    // }

    // [Fact]
    // public async Task PhoneUpdateAsync_WhenEmailIsNotVerified_Returns400BadRequestStatus()
    // {
    //     var request = UserMock.GetExistingUserPhoneUpdateRequest();
    //     var returnedUser = UserMock.GetUnverifiedTutor();
    //     _userService.Setup(u => u.GetUserByEmail(request.Email)).ReturnsAsync(returnedUser);

    //     var result = await _userProfileController.PhoneUpdateAsync(request);

    //     var actionResult = Assert.IsType<ActionResult<GenericResponse>>(result);
    //     var badRequestObjResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
    //     var returnValue = Assert.IsType<GenericResponse>(badRequestObjResult.Value);
    //     Assert.False(returnValue.Success);
    //     Assert.Contains($"{AppMessages.EMAIL} {AppMessages.NOT_VERIFIED}", returnValue.ResultMessage);
    //     _userService.Verify(u => u.PhoneUpdateAsync(request,returnedUser.Id),Times.Never);
    // }
    
    // [Fact]
    // public async Task PhoneUpdateAsync_WhenRoleIsNotSet_Returns400BadRequestStatus()
    // {
    //     var request = UserMock.GetExistingUserPhoneUpdateRequest();
    //     var returnedUser = UserMock.GetNoRoleUser();
    //     _userService.Setup(u => u.GetUserByEmail(request.Email)).ReturnsAsync(returnedUser);

    //     var result = await _userProfileController.PhoneUpdateAsync(request);

    //     var actionResult = Assert.IsType<ActionResult<GenericResponse>>(result);
    //     var badRequestObjResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
    //     var returnValue = Assert.IsType<GenericResponse>(badRequestObjResult.Value);
    //     Assert.False(returnValue.Success);
    //     Assert.Contains($"{AppMessages.USER} {AppMessages.HAS_NO_ROLE}", returnValue.ResultMessage);
    //     _userService.Verify(u => u.PhoneUpdateAsync(request,returnedUser.Id),Times.Never);
    // }
    
    // [Fact]
    // public async Task PhoneUpdateAsync_WhenUserExistsAndPhoneIsValid_Returns200OkRequestStatus()
    // {
    //     var request = UserMock.GetExistingUserPhoneUpdateRequest();
    //     var returnedUser = UserMock.GetValidNewRegistrationTutor();
    //     _userService.Setup(u => u.GetUserByEmail(request.Email)).ReturnsAsync(returnedUser);

    //     _userService.Setup(u => u.PhoneUpdateAsync(request, returnedUser.Id)).ReturnsAsync(UserMock.GetSuccessfulPhoneUpdateResponse());

    //     var result = await _userProfileController.PhoneUpdateAsync(request);

    //     var actionResult = Assert.IsType<ActionResult<GenericResponse>>(result);
    //     var okObjResult = Assert.IsType<OkObjectResult>(actionResult.Result);
    //     var returnValue = Assert.IsType<GenericResponse>(okObjResult.Value);
    //     Assert.True(returnValue.Success);
    //     Assert.Contains($"{AppMessages.MOBILE_PHONE} {AppMessages.UPDATE} {AppMessages.SUCCESSFUL}", returnValue.ResultMessage);
    // }

    #endregion

}