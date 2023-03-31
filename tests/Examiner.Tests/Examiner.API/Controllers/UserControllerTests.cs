using Examiner.API.Controllers;
using Examiner.Application.Authentication.Interfaces;
using Examiner.Application.Users.Interfaces;
using Examiner.Domain.Dtos;
using Examiner.Domain.Dtos.Users;
using Examiner.Tests.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Examiner.Tests.Examiner.API.Controllers;

public class UserControllerTests
{

    private readonly Mock<IAuthenticationService> _authenticationService;
    private readonly Mock<IUserService> _userService;
    private readonly UserController _userController;

    public UserControllerTests()
    {
        _authenticationService = new();
        _userService = new();
        _userController = new UserController(_authenticationService.Object, _userService.Object);
    }

    #region Registrations

    [Fact]
    public async Task RegisterAsync_WhenCalledWithValidRegistrationData_Returns201CreatedStatus()
    {
        var request = UserMock.RegisterTutorWithValidPassword();
        var registeredUserResponse = UserMock.GetNewlyRegisteredUser();
        _authenticationService.Setup(_ => _.RegisterAsync(request)).ReturnsAsync(registeredUserResponse!);

        var result = await _userController.RegisterAsync(request);

        var actionResult = Assert.IsType<ActionResult<GenericResponse>>(result);
        var createdActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
        var returnValue = Assert.IsType<UserResponse>(createdActionResult.Value);
        Assert.True(returnValue.Success);
        Assert.Equal("Registering user was successful, and verification code sent successfully", returnValue.ResultMessage);
        Assert.Null(returnValue.FirstName);
        Assert.Null(returnValue.LastName);
        Assert.Null(returnValue.MobilePhone);
        Assert.NotNull(returnValue.Email);
    }

    [Fact]
    public async Task RegisterAsync_WhenPasswordsDoNotMatch_Returns400BadRequestStatus()
    {
        var request = UserMock.RegisterTutorWithNonMatchingPassword();

        var result = await _userController.RegisterAsync(request);

        var actionResult = Assert.IsType<ActionResult<GenericResponse>>(result);
        Assert.IsType<BadRequestObjectResult>(actionResult.Result);
    }

    [Fact]
    public async Task RegisterAsync_WhenCalledWithInValidRegistrationData_Returns400BadRequestStatus()
    {
        _userController.ModelState.AddModelError("error", "some error");

        var result = await _userController.RegisterAsync(request: null!);

        var actionResult = Assert.IsType<ActionResult<GenericResponse>>(result);
        Assert.IsType<BadRequestObjectResult>(actionResult.Result);
    }




    #endregion
}