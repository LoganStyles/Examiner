using Examiner.Application.Notifications.Interfaces;
using Examiner.Domain.Dtos;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace Examiner.Application.Notifications.Services;

public class EmailService : IEmailService
{
    private readonly IVerificationService _verificationService;
    private readonly ILogger<EmailService> _logger;
    private readonly IConfiguration _configuration;


    private const string FAILED_VERIFICATION = "Email address is not verified";
    private const string EMAIL_SENDING_FAILED = "unable to send email";
    private const string EMAIL_SENDING_SUCCESSFUL = "Email sent successfully";
    private const string SENDER_NAME = "Examina Co.";
    private const string SENDER_EMAIL_ADDRESS = "ch9labsbybincom@gmail.com";
    private const string SMTP_HOST = "smtp.gmail.com";
    private const int SMTP_PORT = 587;

    public EmailService(IVerificationService verificationService, 
    ILogger<EmailService> logger, 
    IConfiguration configuration)
    {
        _verificationService = verificationService;
        _logger = logger;
        _configuration = configuration;
    }

    /// <summary>
    /// Sends an email message to a user
    /// </summary>
    /// <param name="email">The email address to send to</param>
    /// <param name="message">The message to be sent</param>
    /// <returns>An GenericResponse object indicating the success or failure of an attempt to email a message</returns>
    public async Task<GenericResponse> SendMessage(string receiverName, 
    string email, string subject, string htmlMessage)
    {

        var resultResponse = GenericResponse.Result(false, FAILED_VERIFICATION);

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(htmlMessage))
            return resultResponse;

        try
        {
            var verificationResult = await _verificationService.IsVerified(email);
            if (!verificationResult.Success)
            {
                resultResponse.ResultMessage = verificationResult.ResultMessage;
                return resultResponse;
            }

            // send message to email, 
            resultResponse.ResultMessage = EMAIL_SENDING_FAILED;
            /* we will store a record of an email sent */
            SendEmail(receiverName, email, subject, htmlMessage);
            resultResponse.ResultMessage = EMAIL_SENDING_SUCCESSFUL;
            resultResponse.Success = true;
            return resultResponse;
        }
        catch (Exception ex)
        {
            // fetch inner exceptions if exist
            _logger.LogError("Error sending message - ", ex.Message);
            resultResponse.ResultMessage = ex.Message;
            return resultResponse;
        }
    }

    private void SendEmail(string receiverName, string receiverEmail, string subject, string htmlMessage)
    {

        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(SENDER_NAME, SENDER_EMAIL_ADDRESS));
        email.To.Add(new MailboxAddress(receiverName, receiverEmail));
        email.Subject = subject;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = htmlMessage
        };

        using (var smtp = new SmtpClient())
        {
            var smtpUsername = (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("SMTP_USERNAME")))
            ?Environment.GetEnvironmentVariable("SMTP_USERNAME"):_configuration["SMTP_USERNAME"];

            var smtpPassword = (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("SMTP_PASSWORD")))
            ?Environment.GetEnvironmentVariable("SMTP_PASSWORD"):_configuration["SMTP_PASSWORD"];
            
            smtp.Connect(SMTP_HOST, SMTP_PORT, SecureSocketOptions.StartTls);

            smtp.Authenticate(smtpUsername, smtpPassword);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

    }
}