using System.Linq.Expressions;
using Examiner.Application.Notifications.Helpers;
using Examiner.Application.Notifications.Services;
using Examiner.Domain.Entities.Notifications.Emails;
using Examiner.Infrastructure.Repositories;
using Examiner.Infrastructure.UnitOfWork.Interfaces;
using Examiner.Tests.MockData;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;

namespace Examiner.Tests.Examiner.Application.Notifications.Services;

public class KickboxVerificationServiceTests
{

    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly NullLogger<KickboxVerificationService> _logger;
    private readonly KickboxVerificationService _kickboxVerificationService;
    private readonly Mock<IOptions<AppSettings>> _appSettings;

    public const string UNKNOWN = "Unknown (Destination mail server may be temporarily unavailable)";
    public const string INSUFFICIENT_BALANCE = "Unable to complete verification at this moment, possibly due to insufficient balance";
    public const string DELIVERABLE = "deliverable";
    public const string UNDELIVERABLE = "undeliverable";
    public const string RISKY = "risky";
    public const string MAIL_UNKNOWN = "unknown";
    public const string ACCEPTED_EMAIL = "accepted_email";
    public const string VALID_EMAIL = "Email Address is valid";
    public const string INVALID_EMAIL = "Invalid Email Address Supplied";
    public const string UNKNOWN_EMAIL = "Unable to verify Email Address Supplied";

    public KickboxVerificationServiceTests()
    {
        _unitOfWork = new();
        _appSettings = new();
        _logger = new();
        _kickboxVerificationService = new(_logger, _appSettings.Object, _unitOfWork.Object);
    }

    [Fact]
    public async Task IsVerified_WhenCalledWithInvalidEmail_Fails()
    {

        var result = await _kickboxVerificationService.IsVerified(It.IsAny<string>());
        Assert.False(result.Success);
    }

    [Fact]
    public async Task IsVerified_WhenEmailAlreadyExists_DoesNotReturnInvalidEmail()
    {

        var existingResult = KickboxVerificationMock.GetListOfExistingVerifications();
        _unitOfWork.Setup(
                        unit =>
                            unit.KickboxVerificationRepository.Get(
                                It.IsAny<Expression<Func<KickboxVerification, bool>>?>(),
                                It.IsAny<Func<IQueryable<KickboxVerification>, IOrderedQueryable<KickboxVerification>>?>(),
                                It.IsAny<string>(),
                                It.IsAny<int?>(),
                                It.IsAny<int?>()
                            )
                    )
                    .Returns(() => existingResult);

        var result = await _kickboxVerificationService.IsVerified(existingResult.Result.FirstOrDefault()!.Email);
        Assert.NotEqual(INVALID_EMAIL, result.ResultMessage);
    }

    // [Fact]
    // public async Task IsVerified_WhenEmailDoesNotExistInTheDatabase_VerifysEmail()
    // {

    //     var emptyResult = KickboxVerificationMock.GetEmptyListOfExistingVerifications();
    //     _unitOfWork.Setup(
    //                     unit =>
    //                         unit.KickboxVerificationRepository.Get(
    //                             It.IsAny<Expression<Func<KickboxVerification, bool>>?>(),
    //                             It.IsAny<Func<IQueryable<KickboxVerification>, IOrderedQueryable<KickboxVerification>>?>(),
    //                             It.IsAny<string>(),
    //                             It.IsAny<int?>(),
    //                             It.IsAny<int?>()
    //                         )
    //                 )
    //                 .Returns(() => emptyResult);

    //     var result = await _kickboxVerificationService.IsVerified("y@gmail.com");
    //     Assert.NotEqual(INVALID_EMAIL, result.ResultMessage);
    // }
}