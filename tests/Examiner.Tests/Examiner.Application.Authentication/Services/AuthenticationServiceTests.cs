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

    private const string EMAIL_SENDING_FAILED = "unable to send email";
    private const string EMAIL_SENDING_SUCCESSFUL = "Email sent successfully";
    private const string USER_REGISTRATION_SUCCESSFUL = "Registering user was successful, and verification code sent successfully";
    private const string CODE_GENERATION_FAILED = "Unable to generate verification code";


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

    #region Tutor 

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
        Assert.Contains("failed", result.ResultMessage);
    }

    [Fact]
    public async Task RegisterAsync_WhenVerificationCodeIsNotGenerated_Fails()
    {
        var request = UserMock.RegisterTutorWithValidPassword();
        var emptyResult = UserMock.GetEmptyListOfExistingUsers();
        var codeGenerationResponse = new CodeGenerationResponse(false, CODE_GENERATION_FAILED);

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
        Assert.Contains(CODE_GENERATION_FAILED, result.ResultMessage);
    }
    
    [Fact]
    public async Task RegisterAsync_WhenVerificationCodeIsNotSent_Fails()
    {
        var request = UserMock.RegisterTutorWithValidPassword();
        var emptyResult = UserMock.GetEmptyListOfExistingUsers();
        var codeGenerationResponse = new CodeGenerationResponse(true, It.IsAny<string>());
        var codeSendingResultResponse = GenericResponse.Result(false, EMAIL_SENDING_FAILED);

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
        _emailService.Setup(msg => msg.SendMessage(request.Email, It.IsAny<string>())).ReturnsAsync(() => codeSendingResultResponse);

        var result = await _authService.RegisterAsync(request);
        Assert.False(result.Success);
        Assert.Contains(EMAIL_SENDING_FAILED, result.ResultMessage);
    }

    [Fact]
    public async Task RegisterAsync_WhenVerificationCodeIsSent_Succeeds()
    {
        var request = UserMock.RegisterTutorWithValidPassword();
        var emptyResult = UserMock.GetEmptyListOfExistingUsers();
        var codeGenerationResponse = new CodeGenerationResponse(true, It.IsAny<string>());
        var codeSendingResultResponse = GenericResponse.Result(true, EMAIL_SENDING_SUCCESSFUL);

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
        _emailService.Setup(msg => msg.SendMessage(request.Email, It.IsAny<string>())).ReturnsAsync(() => codeSendingResultResponse);
        _unitOfWork.Setup(unit => unit.CodeVerificationRepository.AddAsync(It.IsAny<CodeVerification>())).ReturnsAsync(It.IsAny<CodeVerification>());

        var result = await _authService.RegisterAsync(request);
        Assert.True(result.Success);
        Assert.Contains("verification code sent successfully", result.ResultMessage);
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
        Assert.Contains("Email already exists", result.ResultMessage);
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



    #endregion

    #endregion
}