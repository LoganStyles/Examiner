using System.Linq.Expressions;
using Examiner.Application.Authentication.Interfaces;
using Examiner.Application.Authentication.Services;
using Examiner.Application.Notifications.Interfaces;
using Examiner.Domain.Dtos;
using Examiner.Domain.Dtos.Authentication;
using Examiner.Domain.Entities.Authentication;
using Examiner.Domain.Entities.Users;
using Examiner.Infrastructure.UnitOfWork.Interfaces;
using Examiner.Tests.MockData;
using Examiner.Common;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Examiner.Tests.Examiner.Application.Authentication.Services;

public class AuthenticationServiceTests
{

    private readonly AuthenticationService _authService;
    private readonly Mock<IJwtTokenHandler> _jwtTokenHandler;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly NullLogger<AuthenticationService> _logger;
    private readonly Mock<ICodeService> _codeService;
    private readonly Mock<IEmailService> _emailService;

    public AuthenticationServiceTests()
    {
        _jwtTokenHandler = new();
        _unitOfWork = new();
        _logger = new();
        _codeService = new();
        _emailService = new();

        _authService = new AuthenticationService(
            _jwtTokenHandler.Object,
            _unitOfWork.Object,
            _logger,
            _codeService.Object,
            _emailService.Object
            );
    }


    #region Registrations
    [Fact]
    public async Task RegisterAsync_WithInvalidPassword_Fails()
    {
        var request = UserMock.RegisterTutorWithInvalidPassword();
        var emptyResult = UserMock.GetEmptyListOfExistingUsers();

        _unitOfWork
            .Setup(
                unit =>
                    unit.UserRepository.Get(
                        It.IsAny<Expression<Func<User, bool>>?>(),
                        It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>?>(),
                        It.IsAny<string>(),
                        It.IsAny<int?>(),
                        It.IsAny<int?>()
                    )
            )
            .Returns(() => emptyResult);

        var result = await _authService.RegisterAsync(request);
        Assert.False(result.Success);
        Assert.Contains(AppMessages.INVALID_PASSWORD, result.ResultMessage);
    }

    [Fact]
    public async Task RegisterAsync_WhenVerificationCodeIsNotGenerated_Fails()
    {
        var request = UserMock.RegisterTutorWithValidPassword();
        var emptyResult = UserMock.GetEmptyListOfExistingUsers();
        var codeGenerationResponse = new CodeGenerationResponse(false, $"{AppMessages.CODE_GENERATION} {AppMessages.FAILED}");

        _unitOfWork
            .Setup(
                unit =>
                    unit.UserRepository.Get(
                        It.IsAny<Expression<Func<User, bool>>?>(),
                        It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>?>(),
                        It.IsAny<string>(),
                        It.IsAny<int?>(),
                        It.IsAny<int?>()
                    )
            )
            .Returns(() => emptyResult);

        _codeService.Setup(code => code.GetCode()).ReturnsAsync(() => codeGenerationResponse);

        var result = await _authService.RegisterAsync(request);
        Assert.False(result.Success);
        Assert.Contains($"{AppMessages.CODE_GENERATION} {AppMessages.FAILED}", result.ResultMessage);
    }

    [Fact]
    public async Task RegisterAsync_WhenVerificationCodeIsNotSent_Fails()
    {
        var request = UserMock.RegisterTutorWithValidPassword();
        var emptyResult = UserMock.GetEmptyListOfExistingUsers();
        var codeGenerationResponse = CodeVerificationMock.GetSuccessfulCodeGenerationResponse();
        var codeSendingResultResponse = GenericResponse.Result(false, $"{AppMessages.EMAIL} {AppMessages.SENDING} {AppMessages.FAILED}");

        _unitOfWork
            .Setup(
                unit =>
                    unit.UserRepository.Get(
                        It.IsAny<Expression<Func<User, bool>>?>(),
                        It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>?>(),
                        It.IsAny<string>(),
                        It.IsAny<int?>(),
                        It.IsAny<int?>()
                    )
            )
            .Returns(() => emptyResult);

        _codeService.Setup(code => code.GetCode()).Returns(() => codeGenerationResponse);
        _emailService.Setup(msg => msg.SendMessage("", request.Email, It.IsAny<string>(), It.IsAny<string>()))
        .ReturnsAsync(() => codeSendingResultResponse);

        var result = await _authService.RegisterAsync(request);
        Assert.False(result.Success);
        Assert.Contains($"{AppMessages.EMAIL} {AppMessages.SENDING} {AppMessages.FAILED}", result.ResultMessage);
    }

    [Fact]
    public async Task RegisterAsync_WhenVerificationCodeIsSent_Succeeds()
    {
        var request = UserMock.RegisterTutorWithValidPassword();
        var emptyResult = UserMock.GetEmptyListOfExistingUsers();
        var codeGenerationResponse = new CodeGenerationResponse(true, It.IsAny<string>()) { Code = "780000" };

        var codeSendingResultResponse = GenericResponse.Result(true, It.IsAny<string>());

        _codeService.Setup(code => code.GetCode()).ReturnsAsync(() => codeGenerationResponse);
        _emailService.Setup(msg => msg.SendMessage("", request.Email, It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(() => codeSendingResultResponse);
        _unitOfWork.Setup(unit => unit.CodeVerificationRepository.AddAsync(It.IsAny<CodeVerification>())).ReturnsAsync(It.IsAny<CodeVerification>());
        _unitOfWork.Setup(unit => unit.UserRepository.Get(
                                It.IsAny<Expression<Func<User, bool>>?>(),
                                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>?>(),
                                It.IsAny<string>(),
                                It.IsAny<int?>(),
                                It.IsAny<int?>()
                            )).Returns(() => emptyResult);

        var result = await _authService.RegisterAsync(request);
        Assert.True(result.Success);
        Assert.Contains($"{AppMessages.REGISTRATION} {AppMessages.SUCCESSFUL}", result.ResultMessage);
    }

    [Fact]
    public async Task RegisterAsync_ExistingUserName_Fails()
    {
        var validTutors = UserMock.GetAListOfValidTutors();
        var request = UserMock.RegisterTutorWithValidPassword();

        _unitOfWork
            .Setup(u => u.UserRepository.Get(
                It.IsAny<Expression<Func<User, bool>>?>(),
                        It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>?>(),
                        It.IsAny<string>(),
                        It.IsAny<int?>(),
                        It.IsAny<int?>()
                        ))
            .Returns(() => validTutors);

        var result = await _authService.RegisterAsync(request);
        Assert.False(result.Success);
        Assert.Contains($"{AppMessages.EMAIL} {AppMessages.EXISTS}", result.ResultMessage);
    }

    [Fact]
    public async Task RegisterAsync_ThrowsAnExceptionWhenAddingTutor_Fails()
    {
        var validTutor = UserMock.GetValidTutor();
        var emptyResult = UserMock.GetEmptyListOfExistingUsers();
        var request = UserMock.RegisterTutorWithValidPassword();
        _unitOfWork.Setup(_ => _.UserRepository.AddAsync(validTutor)).Throws<Exception>();
        _unitOfWork
            .Setup(u => u.UserRepository.Get(
                It.IsAny<Expression<Func<User, bool>>?>(),
                        It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>?>(),
                        It.IsAny<string>(),
                        It.IsAny<int?>(),
                        It.IsAny<int?>()
                        ))
            .Returns(() => emptyResult);

        var result = await _authService.RegisterAsync(request);

        Assert.False(result.Success);
    }

    #endregion

    #region Login

    [Fact]
    public async Task Authenticate_AnExistingUser_Succeeds()
    {
        var validUser = UserMock.GetAListOfNewlyRegisteredValidTutors();

        _unitOfWork
            .Setup(
                unit =>
                    unit.UserRepository.Get(
                        It.IsAny<Expression<Func<User, bool>>?>(),
                        It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>?>(),
                        It.IsAny<string>(),
                        It.IsAny<int?>(),
                        It.IsAny<int?>()
                    )
            )
            .Returns(() => validUser);

        var validAuthenticationResponse = UserMock.GetValidAuthenticationResponse();
        var request = UserMock.AuthenticateTutorWithValidPassword();
        _jwtTokenHandler.Setup(jwt => jwt.GenerateJwtToken(request)).Returns(validAuthenticationResponse);

        var result = await _authService.Authenticate(request);

        Assert.True(result.Success);
        // Assert.Contains("successful", result.ResultMessage);
        _jwtTokenHandler.Verify(jwt => jwt.GenerateJwtToken(request), Times.Once);
    }

    [Fact]
    public async Task Authenticate_ByNonActiveUser_Succeeds()
    {
        var nonActiveUser = UserMock.GetAListOfNewlyRegisteredValidTutors();
        _unitOfWork
            .Setup(
                unit =>
                    unit.UserRepository.Get(
                        It.IsAny<Expression<Func<User, bool>>?>(),
                        It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>?>(),
                        It.IsAny<string>(),
                        It.IsAny<int?>(),
                        It.IsAny<int?>()
                    )
            )
            .Returns(() => nonActiveUser);

        var request = UserMock.AuthenticateTutorWithValidPassword();
        var nonActiveUserResponse = UserMock.GetValidAuthenticationResponse();
        _jwtTokenHandler.Setup(jwt => jwt.GenerateJwtToken(request)).Returns(nonActiveUserResponse);

        var result = await _authService.Authenticate(request);

        Assert.True(result.Success);
        Assert.Contains($"{AppMessages.USER} {AppMessages.ACCOUNT_NOT_VERIFIED}", result.ResultMessage);
    }

    [Fact]
    public async Task Authenticate_ByNonRoleUser_Succeeds()
    {
        var nonRoleUser = UserMock.GetAListOfNewlyRegisteredValidTutorsWithoutRole();
        _unitOfWork
            .Setup(
                unit =>
                    unit.UserRepository.Get(
                        It.IsAny<Expression<Func<User, bool>>?>(),
                        It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>?>(),
                        It.IsAny<string>(),
                        It.IsAny<int?>(),
                        It.IsAny<int?>()
                    )
            )
            .Returns(() => nonRoleUser);

        var request = UserMock.AuthenticateTutorWithValidPassword();
        var nonRoleUserResponse = UserMock.GetValidAuthenticationResponse();
        _jwtTokenHandler.Setup(jwt => jwt.GenerateJwtToken(request)).Returns(nonRoleUserResponse);

        var result = await _authService.Authenticate(request);

        Assert.True(result.Success);
        Assert.Contains($"{AppMessages.USER} {AppMessages.HAS_NO_ROLE}", result.ResultMessage);
    }


    [Fact]
    public async Task Authenticate_ByNonExistingUser_Fails()
    {
        var invalidUser = UserMock.GetEmptyListOfExistingUsers();

        _unitOfWork
            .Setup(
                unit =>
                    unit.UserRepository.Get(
                        It.IsAny<Expression<Func<User, bool>>?>(),
                        It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>?>(),
                        It.IsAny<string>(),
                        It.IsAny<int?>(),
                        It.IsAny<int?>()
                    )
            )
            .Returns(() => invalidUser);

        var request = UserMock.AuthenticateTutorWithValidPassword();

        var result = await _authService.Authenticate(request);

        _jwtTokenHandler.Verify(jwt => jwt.GenerateJwtToken(request), Times.Never);
        Assert.False(result.Success);
        Assert.Contains(AppMessages.NOT_EXIST, result.ResultMessage);
    }

    [Fact]
    public async Task Authenticate_WithNonExistingCredentials_Fails()
    {
        var invalidUser = UserMock.GetAListOfNewlyRegisteredValidTutors();

        _unitOfWork
            .Setup(
                unit =>
                    unit.UserRepository.Get(
                        It.IsAny<Expression<Func<User, bool>>?>(),
                        It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>?>(),
                        It.IsAny<string>(),
                        It.IsAny<int?>(),
                        It.IsAny<int?>()
                    )
            )
            .Returns(() => invalidUser);

        var request = UserMock.AuthenticateTutorWithNonExistingPassword();

        var result = await _authService.Authenticate(request);

        _jwtTokenHandler.Verify(jwt => jwt.GenerateJwtToken(request), Times.Never);
        Assert.False(result.Success);
        Assert.Contains(AppMessages.INVALID_EMAIL_PASSWORD, result.ResultMessage);
    }


    #endregion

    #region change password

    [Fact]
    public async Task ChangePasswordAsync_WithInvalidUsername_ReturnsFailedResponse()
    {
        var request = It.IsAny<ChangePasswordRequest>();
        var invalidUser = UserMock.GetEmptyListOfExistingUsers();
        _unitOfWork
            .Setup(
                unit =>
                    unit.UserRepository.Get(
                        It.IsAny<Expression<Func<User, bool>>?>(),
                        It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>?>(),
                        It.IsAny<string>(),
                        It.IsAny<int?>(),
                        It.IsAny<int?>()
                    )
            )
            .Returns(() => invalidUser);

        var result = await _authService.ChangePasswordAsync(request);

        Assert.False(result.Success);
        Assert.Contains($"{AppMessages.USER} {AppMessages.NOT_EXIST}", result.ResultMessage);
        _unitOfWork.Verify(_ => _.UserRepository.Update(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task ChangePasswordAsync_WhenOldPasswordFieldDoesNotMatchExistingPassword_ReturnsFailedResponse()
    {
        var validUser = UserMock.GetAListOfValidTutors();
        _unitOfWork
            .Setup(
                unit =>
                    unit.UserRepository.Get(
                        It.IsAny<Expression<Func<User, bool>>?>(),
                        It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>?>(),
                        It.IsAny<string>(),
                        It.IsAny<int?>(),
                        It.IsAny<int?>()
                    )
            ).Returns(() => validUser);

        var request = UserMock.GetNonExistingPasswordChangePasswordRequest();
        var result = await _authService.ChangePasswordAsync(request);

        Assert.False(result.Success);
        Assert.Contains(AppMessages.INVALID_EMAIL_PASSWORD, result.ResultMessage);
        _unitOfWork.Verify(_ => _.UserRepository.Update(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task ChangePasswordAsync_WhenNewPasswordDoesNotMeetRequirements_ReturnsFailedResponse()
    {
        var validUser = UserMock.GetAListOfValidTutors();
        _unitOfWork
            .Setup(
                unit =>
                    unit.UserRepository.Get(
                        It.IsAny<Expression<Func<User, bool>>?>(),
                        It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>?>(),
                        It.IsAny<string>(),
                        It.IsAny<int?>(),
                        It.IsAny<int?>()
                    )
            ).Returns(() => validUser);

        var request = UserMock.GetNonValidPasswordParamsChangePasswordRequest();
        var result = await _authService.ChangePasswordAsync(request);

        Assert.False(result.Success);
        Assert.Contains(AppMessages.INVALID_PASSWORD, result.ResultMessage);
        _unitOfWork.Verify(_ => _.UserRepository.Update(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task ChangePasswordAsync_WithValidOldAndNewPasswords_ReturnsSuccess()
    {
        var validUser = UserMock.GetAListOfValidTutors();
        _unitOfWork
            .Setup(
                unit =>
                    unit.UserRepository.Get(
                        It.IsAny<Expression<Func<User, bool>>?>(),
                        It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>?>(),
                        It.IsAny<string>(),
                        It.IsAny<int?>(),
                        It.IsAny<int?>()
                    )
            ).Returns(() => validUser);

        var request = UserMock.GetValidChangePasswordRequest();
        var result = await _authService.ChangePasswordAsync(request);

        Assert.True(result.Success);
        Assert.Contains($"{AppMessages.CHANGE_PASSWORD} {AppMessages.SUCCESSFUL}", result.ResultMessage);
        _unitOfWork.Verify(_ => _.UserRepository.Update(It.IsAny<User>()), Times.Once);
    }

    #endregion

    #region select role

    /* - when email is not found
       - when email is found but role is invalid
       - when email is found & role is valid */

    [Fact]
    public async Task SelectRoleAsync_WhenEmailDoesNotExist_ReturnsFailedResponse()
    {
        var userNotFound = UserMock.GetEmptyListOfExistingUsers();
        _unitOfWork
            .Setup(
                unit =>
                    unit.UserRepository.Get(
                        It.IsAny<Expression<Func<User, bool>>?>(),
                        It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>?>(),
                        It.IsAny<string>(),
                        It.IsAny<int?>(),
                        It.IsAny<int?>()
                    )
            ).Returns(() => userNotFound);

        var request = UserMock.GetNonExistingEmailSelectRoleRequest();
        var result = await _authService.SelectRoleAsync(request);

        Assert.False(result.Success);
        Assert.Contains($"{AppMessages.USER} {AppMessages.NOT_EXIST}", result.ResultMessage);
        _unitOfWork.Verify(_ => _.UserRepository.Update(It.IsAny<User>()), Times.Never);
    }
    
    [Fact]
    public async Task SelectRoleAsync_WhenEmailExistsButRoleDoesNot_ReturnsFailedResponse()
    {
        var existUser = UserMock.GetAListOfValidTutors();
        _unitOfWork
            .Setup(
                unit =>
                    unit.UserRepository.Get(
                        It.IsAny<Expression<Func<User, bool>>?>(),
                        It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>?>(),
                        It.IsAny<string>(),
                        It.IsAny<int?>(),
                        It.IsAny<int?>()
                    )
            ).Returns(() => existUser);

        var request = UserMock.GetNonExistingRoleSelectRoleRequest();
        var result = await _authService.SelectRoleAsync(request);

        Assert.False(result.Success);
        Assert.Contains(AppMessages.INVALID_REQUEST, result.ResultMessage);
        _unitOfWork.Verify(_ => _.UserRepository.Update(It.IsAny<User>()), Times.Never);
    }
    
    [Fact]
    public async Task SelectRoleAsync_WhenEmailExistsAndRoleIsValid_ReturnsSuccess()
    {
        var existUser = UserMock.GetAListOfValidTutors();
        _unitOfWork
            .Setup(
                unit =>
                    unit.UserRepository.Get(
                        It.IsAny<Expression<Func<User, bool>>?>(),
                        It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>?>(),
                        It.IsAny<string>(),
                        It.IsAny<int?>(),
                        It.IsAny<int?>()
                    )
            ).Returns(() => existUser);

        var request = UserMock.GetExistingEmailAndRoleSelectRoleRequest();
        var result = await _authService.SelectRoleAsync(request);

        Assert.True(result.Success);
        Assert.Contains($"{AppMessages.ROLE} {AppMessages.SUCCESSFUL}", result.ResultMessage);
        _unitOfWork.Verify(_ => _.UserRepository.Update(It.IsAny<User>()), Times.Once);
    }

    #endregion
}