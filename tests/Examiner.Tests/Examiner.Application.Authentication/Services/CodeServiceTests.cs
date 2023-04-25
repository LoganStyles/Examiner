using System.Linq.Expressions;
using Examiner.Application.Authentication.Services;
using Examiner.Common;
using Examiner.Domain.Entities.Authentication;
using Examiner.Domain.Entities.Users;
using Examiner.Infrastructure.UnitOfWork.Interfaces;
using Examiner.Tests.MockData;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Examiner.Tests.Examiner.Application.Authentication.Services;

public class CodeServiceTests
{

    private readonly CodeService _codeService;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly NullLogger<CodeService> _logger;
    private const string CODE_SENDING_FAILED = "Unable to send verification code";
    private const string CODE_SENDING_SUCCESSFUL = "Verification code sent successfully";
    private const int CODE_LENGTH = 6;

    public CodeServiceTests()
    {
        _unitOfWork = new();
        _logger = new();
        _codeService = new(_unitOfWork.Object, _logger);
    }

    // how to test for when code generation fails?
    // how to test that generated code does not match an existing valid code?

    [Fact]
    public async Task GetCode_WhenCalled_GeneratesValidCode()
    {
        var emptyResult = CodeVerificationMock.GetEmptyListOfExistingCodes();

        _unitOfWork
            .Setup(
                unit =>
                    unit.CodeVerificationRepository.Get(
                        It.IsAny<Expression<Func<CodeVerification, bool>>?>(),
                        It.IsAny<Func<IQueryable<CodeVerification>, IOrderedQueryable<CodeVerification>>?>(),
                        It.IsAny<string>(),
                        It.IsAny<int?>(),
                        It.IsAny<int?>()
                    )
            )
            .Returns(() => emptyResult);

        var result = await _codeService.CreateCode();
        Assert.True(result.Success);
        Assert.NotNull(result.Code);
        Assert.Equal(CODE_LENGTH, result.Code.Length);
    }

    [Fact]
    public async Task VerifyCode_FailedAttempt_IncreasesAttemptCount()
    {
        var userList = UserMock.GetListOfRegisteredTutorWithExpiredCodeRequestingCodeVerification();
        var user = userList.FirstOrDefault();
        var previousAttemptCount = user!.CodeVerification!.Attempts;
        var suppliedCode = user!.CodeVerification!.Code;
        var getCodeList = CodeVerificationMock.GetExistingCodeVerificationHavingExpiredCode();

        _unitOfWork.Setup(unit => unit.UserRepository.Update(user)).Returns(Task.CompletedTask);

        _unitOfWork
            .Setup(
                unit =>
                    unit.CodeVerificationRepository.Get(
                        It.IsAny<Expression<Func<CodeVerification, bool>>?>(),
                        It.IsAny<Func<IQueryable<CodeVerification>, IOrderedQueryable<CodeVerification>>?>(),
                        It.IsAny<string>(),
                        It.IsAny<int?>(),
                        It.IsAny<int?>()
                    )
            )
            .ReturnsAsync(() => getCodeList);

        var result = await _codeService.VerifyCode(user!, suppliedCode!);

        _unitOfWork.Verify(u => u.UserRepository.Update(user!), Times.Once);
        Assert.Equal((double)previousAttemptCount + 1, (double)user!.CodeVerification!.Attempts);
        Assert.False(result.Success);
        Assert.Contains($"{AppMessages.CODE_SUPPLIED} {AppMessages.EXPIRED}", result.ResultMessage);
    }
    
    [Fact]
    public async Task VerifyCode_ExpiredAttempts_IncreasesAttemptCount()
    {
        var userList = UserMock.GetListOfRegisteredTutorWithExpiredCodeRequestingCodeVerification();
        var user = userList.FirstOrDefault();
        user!.CodeVerification!.Expired=true;
        var previousAttemptCount = user!.CodeVerification!.Attempts;
        var suppliedCode = user!.CodeVerification!.Code;
        var getCodeList = CodeVerificationMock.GetExistingCodeVerificationHavingExpiredCodeAndExpiredAttempts();

        _unitOfWork.Setup(unit => unit.UserRepository.Update(user)).Returns(Task.CompletedTask);

        _unitOfWork
            .Setup(
                unit =>
                    unit.CodeVerificationRepository.Get(
                        It.IsAny<Expression<Func<CodeVerification, bool>>?>(),
                        It.IsAny<Func<IQueryable<CodeVerification>, IOrderedQueryable<CodeVerification>>?>(),
                        It.IsAny<string>(),
                        It.IsAny<int?>(),
                        It.IsAny<int?>()
                    )
            )
            .ReturnsAsync(() => getCodeList);

        var result = await _codeService.VerifyCode(user!, suppliedCode!);

        _unitOfWork.Verify(u => u.UserRepository.Update(user!), Times.Once);
        Assert.Equal((double)previousAttemptCount + 1, (double)user!.CodeVerification!.Attempts);
        Assert.False(result.Success);
        Assert.Contains($"{AppMessages.CODE_SUPPLIED} {AppMessages.EXPIRED}", result.ResultMessage);
    }
    
    // [Fact]
    // public async Task VerifyCode_NullUserOrCode_ThrowsException()
    // {
    //     // var userList = UserMock.GetListOfRegisteredTutorWithExpiredCodeRequestingCodeVerification();
    //     // var user = userList.FirstOrDefault();
    //     // user!.CodeVerification!.Expired=true;
    //     // var previousAttemptCount = user!.CodeVerification!.Attempts;
    //     // var suppliedCode = user!.CodeVerification!.Code;
    //     // var getCodeList = CodeVerificationMock.GetExistingCodeVerificationHavingExpiredCodeAndExpiredAttempts();

    //     _unitOfWork.Setup(unit => unit.UserRepository.Update(null!)).Returns(Task.CompletedTask);

    //     // _unitOfWork
    //     //     .Setup(
    //     //         unit =>
    //     //             unit.CodeVerificationRepository.Get(
    //     //                 It.IsAny<Expression<Func<CodeVerification, bool>>?>(),
    //     //                 It.IsAny<Func<IQueryable<CodeVerification>, IOrderedQueryable<CodeVerification>>?>(),
    //     //                 It.IsAny<string>(),
    //     //                 It.IsAny<int?>(),
    //     //                 It.IsAny<int?>()
    //     //             )
    //     //     )
    //     //     .ReturnsAsync(() => getCodeList);

    //     var result = await _codeService.VerifyCode(user:null!, suppliedCode:null!);
    //     Assert.Throws<InvalidOperationException>();

    //     _unitOfWork.Verify(u => u.UserRepository.Update(It.IsAny<User>()), Times.Never);
    //     // Assert.Equal((double)previousAttemptCount + 1, (double)user!.CodeVerification!.Attempts);
    //     Assert.False(result.Success);
    //     Assert.Contains($"{AppMessages.CODE_SUPPLIED} {AppMessages.EXPIRED}", result.ResultMessage);
    // }
    
    [Fact]
    public async Task VerifyCode_SuccessfulAttempt_DoesNotIncreasesAttemptCount()
    {
        var userList = UserMock.GetListOfRegisteredTutorWithValidCodeRequestingCodeVerification();
        var user = userList.FirstOrDefault();
        var previousAttemptCount = user!.CodeVerification!.Attempts;
        var suppliedCode = user!.CodeVerification!.Code;
        var getCodeList = CodeVerificationMock.GetExistingCodeVerificationHavingValidCode();

        _unitOfWork.Setup(unit => unit.UserRepository.Update(user)).Returns(Task.CompletedTask);

        _unitOfWork
            .Setup(
                unit =>
                    unit.CodeVerificationRepository.Get(
                        It.IsAny<Expression<Func<CodeVerification, bool>>?>(),
                        It.IsAny<Func<IQueryable<CodeVerification>, IOrderedQueryable<CodeVerification>>?>(),
                        It.IsAny<string>(),
                        It.IsAny<int?>(),
                        It.IsAny<int?>()
                    )
            )
            .ReturnsAsync(() => getCodeList);

        var result = await _codeService.VerifyCode(user!, suppliedCode!);

        _unitOfWork.Verify(u => u.UserRepository.Update(user!), Times.Once);
        Assert.Equal((double)previousAttemptCount + 1, (double)user!.CodeVerification!.Attempts);
        Assert.True(result.Success);
        Assert.Contains($"{AppMessages.CODE_VERIFICATION} {AppMessages.SUCCESSFUL}", result.ResultMessage);
    }


}