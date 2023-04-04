using System.Text.RegularExpressions;
using Examiner.Application.Notifications.Interfaces;
using Examiner.Authentication.Domain.Mappings;
using Examiner.Domain.Dtos;
using Examiner.Domain.Entities.Notifications.Emails;
using Examiner.Infrastructure.UnitOfWork.Interfaces;
using Kickbox;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Examiner.Application.Notifications.Services;

/// <summary>
/// Implements contract for verifying emails with Kickbox
/// </summary>
public class KickboxVerificationService : IVerificationService
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<KickboxVerificationService> _logger;
    private readonly IConfiguration _configuration;

    private const string UNKNOWN = "Unknown (Destination mail server may be temporarily unavailable)";
    private const string INSUFFICIENT_BALANCE = "Unable to complete verification at this moment, possibly due to insufficient balance";
    private const string DELIVERABLE = "deliverable";
    private const string UNDELIVERABLE = "undeliverable";
    private const string RISKY = "risky";
    private const string MAIL_UNKNOWN = "unknown";
    private const string ACCEPTED_EMAIL = "accepted_email";
    private const string VALID_EMAIL = "Email Address is valid";
    private const string VALID_EMAIL_FORMAT = "Email Address has valid format";
    private const string INVALID_EMAIL = "Invalid Email Address Supplied";
    private const string UNKNOWN_EMAIL = "Unable to verify Email Address Supplied";


    public KickboxVerificationService(
        ILogger<KickboxVerificationService> logger,
        IUnitOfWork unitOfWork,
        IConfiguration configuration)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    /// <summary>
    /// Determines if a user's email is verified
    /// </summary>
    /// <param name="email">A string representing the email to be verified</param>
    /// <returns>A KickboxResponse indicating the success or failure of the verification</returns>
    public async Task<GenericResponse> IsVerified(string email)
    {
        var response = new GenericResponse(false, INVALID_EMAIL);
        try
        {

            if (!IsValidFormat(email))
                return response;

            var existingKickboxVerificationList = await _unitOfWork.KickboxVerificationRepository
            .Get(
                (verification => verification.Email == email && verification.Success)
                , null, "", null, null);

            var verification = existingKickboxVerificationList.LastOrDefault();
            if (verification is not null)
            {
                // An existing verification is found
                response.Success = verification.IsValidEmail;
                response.ResultMessage = verification.SupportingMessage;
                return response;
            }

            // no previous verification exists so perform a new one
            var verificationResult = await Verify(email);
            response.ResultMessage = verificationResult.SupportingMessage;
            // save only if request went through & returned
            // also - verificationResult.Success means we accessed kickbox successfully
            // i.e verificationResult.Success differs from response.Success
            if (verificationResult.Success)
            {
                response.Success = verificationResult.IsValidEmail;
                // insert into table for future verification requests
                await _unitOfWork.KickboxVerificationRepository.AddAsync(verificationResult);
                await _unitOfWork.CompleteAsync();
            }
            return response;

        }
        catch (Exception ex)
        {
            _logger.LogError($"Error verifying {email} - ", ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Verifies a user's email
    /// </summary>
    /// <param name="email">A string representing the email to be verified</param>
    /// <returns>A KickboxVerification object indicating details for the verification</returns>
    private async Task<KickboxVerification> Verify(string email)
    {

        var kickboxKey = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("KICKBOX_PROD_KEY"))
        ? _configuration["KICKBOX_PROD_KEY"] : Environment.GetEnvironmentVariable("KICKBOX_PROD_KEY");
        var kickBoxApi = new KickBoxApi(kickboxKey);
        var verification = new KickboxVerification();

        try
        {
            var kickboxResponse = await kickBoxApi.VerifyWithResponse(email);

            if (kickboxResponse is not null)
            {
                verification = ObjectMapper.Mapper.Map<KickboxVerification>(kickboxResponse);

                if (!verification.Success)
                {
                    // request did not go through probably due to insufficient balance
                    verification.SupportingMessage = INSUFFICIENT_BALANCE;
                    return verification;
                }
                else
                {
                    /* since verification.Result can have multiple values,
                    i am adding verification.SupportingMessage to provide more 
                    clarity in determining a valid email */

                    if (verification.Result is not null && verification.Result.ToLower() == RISKY)
                        verification.SupportingMessage = RISKY;

                    else if (verification.Result is not null && verification.Result.ToLower() == UNDELIVERABLE)
                        verification.SupportingMessage = UNDELIVERABLE;

                    else if (verification.Result is not null && verification.Result.ToLower() == DELIVERABLE
                    && verification.Reason is not null && verification.Reason.ToLower() == ACCEPTED_EMAIL)
                    {
                        verification.SupportingMessage = VALID_EMAIL;
                        verification.IsValidEmail = true;
                    }
                    else
                        verification.SupportingMessage = UNKNOWN;
                }

            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception Caught in (KickboxVerificationService.Verify) => " + ex.Message);
            // throw ex;
        }
        return verification;
    }

    private bool IsValidFormat(string email)
    {
        var validFormatStatus = false;
        email = TrimEmail(email);

        if (!String.IsNullOrWhiteSpace(email))
        {
            try
            {
                validFormatStatus = Regex.IsMatch(email, "^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250.0));
            }
            catch (RegexMatchTimeoutException ex)
            {
                _logger.LogError(ex, "Exception in (VerificationService.IsValidFormat) => " + ex.Message);
                return validFormatStatus;
            }
        }
        return validFormatStatus;
    }

    private string TrimEmail(string email)
    {
        string trimmedEmail = string.Empty;
        if (!string.IsNullOrWhiteSpace(email))
            trimmedEmail = email.Trim();
        return trimmedEmail;
    }

    public Task<GenericResponse> IsValid(string channel)
    {
        var validStatus = this.IsValidFormat(channel);
        if (validStatus)
            return Task.FromResult(GenericResponse.Result(true, VALID_EMAIL_FORMAT));
        else
            return Task.FromResult(GenericResponse.Result(false, INVALID_EMAIL));
    }

}