using System.Linq.Expressions;
using Examiner.Application.Authentication.Services;
using Examiner.Domain.Entities.Authentication;
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
}