using Examiner.Application.Notifications.Interfaces;
using Examiner.Domain.Dtos;

namespace Examiner.Application.Notifications.Services;

public class EmailService : IEmailService
{
    private readonly IVerificationService _verificationService;

    private const string FAILED_VERIFICATION = "Email address is not verified";
    private const string EMAIL_SENDING_FAILED = "unable to send email";
    private const string EMAIL_SENDING_SUCCESSFUL = "Email sent successfully";

    public EmailService(IVerificationService verificationService)
    {
        _verificationService = verificationService;
    }

    /// <summary>
    /// Sends an email message to a user
    /// </summary>
    /// <param name="email">The email of the user</param>
    /// <param name="message">The message to be sent</param>
    /// <returns>An GenericResponse object indicating the success or failure of an attempt to email a message</returns>
    public async Task<GenericResponse> SendMessage(string email, string message)
    {

        var resultResponse = GenericResponse.Result(false, FAILED_VERIFICATION);

        var verificationResult = await _verificationService.IsVerified(email);
        if (!verificationResult.Success)
        {
            resultResponse.ResultMessage = verificationResult.ResultMessage;
            return resultResponse;
        }

        // send message to email, 
        resultResponse.ResultMessage = EMAIL_SENDING_FAILED;
        /* we will store a record of an email sent */
        return resultResponse;

    }

}