using Examiner.Application.Notifications.Interfaces;
using Examiner.Application.Notifications.Services;
using Examiner.Domain.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Examiner.Tests.Examiner.Application.Notifications.Services;

public class EmailServiceTests
{

    private readonly Mock<IVerificationService> _verificationService;
    private readonly Mock<IConfiguration> _configuration;
    private readonly EmailService _emailService;
    private readonly NullLogger<EmailService> _logger;

    private const string FAILED_VERIFICATION = "Email address is not verified";
    private const string EMAIL_SENDING_FAILED = "unable to send email";
    private const string EMAIL_SENDING_SUCCESSFUL = "Email sent successfully";
    public EmailServiceTests()
    {
        _verificationService = new();
        _logger = new();
        _configuration=new();
        _emailService = new(_verificationService.Object, _logger,_configuration.Object);
    }

    [Fact]
    public async Task SendMessage_WithNullOrEmptyParams_Fails()
    {

        var result = await _emailService.SendMessage("", "","","");
        Assert.False(result.Success);
    }

    [Fact]
    public async Task SendMessage_WhenEmailIsNotVerified_Fails()
    {
        var verificationResponse = GenericResponse.Result(false, It.IsAny<string>());
        _verificationService.Setup(v => v.IsVerified(It.IsAny<string>())).ReturnsAsync(verificationResponse);

        var result = await _emailService.SendMessage("adam","e@gmail.com","","");
        Assert.False(result.Success);
    }
    
    [Fact]
    public async Task SendMessage_WhenEmailIsNotSent_Fails()
    {
        var verificationResponse = GenericResponse.Result(true, It.IsAny<string>());
        _verificationService.Setup(v => v.IsVerified(It.IsAny<string>())).ReturnsAsync(verificationResponse);

        var result = await _emailService.SendMessage("adam","e@gmail.com","","");
        Assert.False(result.Success);
    }
}