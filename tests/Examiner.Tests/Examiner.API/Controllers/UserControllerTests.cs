using Examiner.API.Controllers;
using Examiner.Application.Authentication.Interfaces;
using Examiner.Application.Users.Interfaces;
using Examiner.Common;
using Examiner.Domain.Dtos;
using Examiner.Domain.Dtos.Authentication;
using Examiner.Domain.Dtos.Users;
using Examiner.Tests.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Examiner.Tests.Examiner.API.Controllers;

public class UserControllerTests
{

    private readonly Mock<IAuthenticationService> _authenticationService;
    private readonly Mock<IUserService> _userService;
    private readonly Mock<ICodeService> _codeService;
    private readonly UserController _userController;

    public UserControllerTests()
    {
        _authenticationService = new();
        _userService = new();
        _codeService = new();
        _userController = new UserController(_authenticationService.Object, _userService.Object,_codeService.Object);
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

    #region Login

    [Fact]
    public async Task LoginAsync_WhenCalledWithValidAuthenticationRequest_Returns200OkRequestStatus()
    {
        var request = UserMock.AuthenticateTutorWithValidPassword();
        var response = UserMock.GetSuccessfulAuthenticationResponse();

        _authenticationService.Setup(_ => _.Authenticate(request)).Returns(response);

        var result = await _userController.LoginAsync(request);

        var actionResult = Assert.IsType<ActionResult<GenericResponse>>(result);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnValue = Assert.IsType<AuthenticationResponse>(okObjectResult.Value);
        Assert.True(returnValue.Success);
        // Assert.Equal("Authenticating user was successful", returnValue.ResultMessage);
    }

    [Fact]
    public async Task LoginAsync_WhenCalledWithInvalidAuthenticationRequest_Returns400BadRequestStatus()
    {

        var request = UserMock.GetInvalidAuthenticationRequest();
        _userController.ModelState.AddModelError("error", "some error");

        var result = await _userController.LoginAsync(request: null!);

        var actionResult = Assert.IsType<ActionResult<GenericResponse>>(result);
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task LoginAsync_WhenCalledWithWrongCredentials_Returns401UnauthorizedRequestStatus()
    {
        var request = UserMock.GetInvalidAuthenticationRequest();
        var response = UserMock.GetFailedUserResponse();
        _authenticationService.Setup(_ => _.Authenticate(request)).Returns(response);

        var result = await _userController.LoginAsync(request);

        var actionResult = Assert.IsType<ActionResult<GenericResponse>>(result);
        Assert.IsType<UnauthorizedObjectResult>(actionResult.Result);
    }

    #endregion

    #region change password

    [Fact]
    public async Task ChangePasswordAsync_InvalidModel_Returns400BadRequest()
    {

        _userController.ModelState.AddModelError("error", "some error");
        var result = await _userController.ChangePasswordAsync(request: null!);

        var actionResult = Assert.IsType<ActionResult<GenericResponse>>(result);
        Assert.IsType<BadRequestObjectResult>(actionResult.Result);
    }

    [Fact]
    public async Task ChangePasswordAsync_NonMatchingPasswords_Returns400BadRequestStatus()
    {

        var request = UserMock.GetNonMatchingPasswordsRequest();

        var result = await _userController.ChangePasswordAsync(request);

        var actionResult = Assert.IsType<ActionResult<GenericResponse>>(result);
        Assert.IsType<BadRequestObjectResult>(actionResult.Result);
    }

    [Fact]
    public async Task ChangePasswordAsync_WhenUserIsNotFound_Returns404NotFoundRequestStatus()
    {
        var request = UserMock.GetValidChangePasswordRequest();
        var response = UserMock.GetFailedUserResponse();
        _authenticationService.Setup(_ => _.ChangePasswordAsync(request)).Returns(response);

        var result = await _userController.ChangePasswordAsync(request);

        var actionResult = Assert.IsType<ActionResult<GenericResponse>>(result);
        var notFoundObjResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
    }

    [Fact]
    public async Task ChangePasswordAsync_Returns200OkRequestStatus()
    {
        var request = UserMock.GetValidChangePasswordRequest();
        var response = UserMock.GetValidUserResponse();
        _authenticationService.Setup(_ => _.ChangePasswordAsync(request)).Returns(response);

        var result = await _userController.ChangePasswordAsync(request);

        var actionResult = Assert.IsType<ActionResult<GenericResponse>>(result);
        var okObjResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnValue = Assert.IsType<GenericResponse>(okObjResult.Value);
        Assert.True(returnValue.Success);
    }

    #endregion
    
    #region select role

    [Fact]
    public async Task SelectRoleAsync_InvalidModel_Returns400BadRequest()
    {

        _userController.ModelState.AddModelError("error", "some error");
        var result = await _userController.SelectRoleAsync(request: null!);

        var actionResult = Assert.IsType<ActionResult<GenericResponse>>(result);
        Assert.IsType<BadRequestObjectResult>(actionResult.Result);
    }

    [Fact]
    public async Task SelectRoleAsync_WhenEmailIsNotFound_Returns404NotFoundRequestStatus()
    {
        var request = UserMock.GetNonExistingEmailSelectRoleRequest();
        var response = UserMock.GetFailedUserResponse();
        _authenticationService.Setup(_ => _.SelectRoleAsync(request)).Returns(response);

        var result = await _userController.SelectRoleAsync(request);

        var actionResult = Assert.IsType<ActionResult<GenericResponse>>(result);
        var notFoundObjResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
    }
    
    [Fact]
    public async Task SelectRoleAsync_WhenEmailIsFoundButRoleIsInvalid_Returns404NotFoundRequestStatus()
    {
        var request = UserMock.GetNonExistingRoleSelectRoleRequest();
        var response = UserMock.GetInvalidRequestGenericResponse();
        _authenticationService.Setup(_ => _.SelectRoleAsync(request)).Returns(response);

        var result = await _userController.SelectRoleAsync(request);

        var actionResult = Assert.IsType<ActionResult<GenericResponse>>(result);
        var notFoundObjResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
    }

    [Fact]
    public async Task SelectRoleAsync_WhenEmailIsFoundAndRoleIsValid_Returns200OkRequestStatus()
    {
        var request = UserMock.GetExistingEmailAndRoleSelectRoleRequest();
        var response = UserMock.GetValidRoleSelectGenericResponse();
        _authenticationService.Setup(_ => _.SelectRoleAsync(request)).Returns(response);

        var result = await _userController.SelectRoleAsync(request);

        var actionResult = Assert.IsType<ActionResult<GenericResponse>>(result);
        var okObjResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnValue = Assert.IsType<GenericResponse>(okObjResult.Value);
        Assert.True(returnValue.Success);
        Assert.Contains($"{AppMessages.ROLE} {AppMessages.SUCCESSFUL}",returnValue.ResultMessage);
    }

    #endregion
}