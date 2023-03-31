using System.Text.RegularExpressions;
using Examiner.Application.Notifications.Helpers;
using Examiner.Application.Notifications.Interfaces;
using Examiner.Authentication.Domain.Mappings;
using Examiner.Domain.Dtos;
using Examiner.Domain.Dtos.Notifications;
using Examiner.Domain.Entities.Notifications.Emails;
using Examiner.Infrastructure.UnitOfWork.Interfaces;
using Kickbox;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Examiner.Application.Notifications.Services;

/// <summary>
/// Implements contract for verifying emails with Kickbox
/// </summary>
public class KickboxVerificationService : IVerificationService
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<KickboxVerificationService> _logger;
    private readonly AppSettings _appSettings;

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

    public KickboxVerificationService(
        ILogger<KickboxVerificationService> logger,
        IOptions<AppSettings> appSettings,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _appSettings = appSettings.Value;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Determines if a user's email is verified
    /// </summary>
    /// <param name="email">A string representing the email to be verified</param>
    /// <returns>A KickboxResponse indicating the success or failure of the verification</returns>
    public async Task<KickboxResponse> IsVerified(string email)
    {
        var response = new KickboxResponse(false, INVALID_EMAIL);
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
            // here - verificationResult.Success means we accessed kickbox successfully
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
            throw ex;
        }
    }

    /// <summary>
    /// Verifies a user's email
    /// </summary>
    /// <param name="email">A string representing the email to be verified</param>
    /// <returns>A KickboxVerification object indicating details for the verification</returns>
    private async Task<KickboxVerification> Verify(string email)
    {

        var kickBoxApi = new KickBoxApi(_appSettings.KickboxKey);
        var verification = new KickboxVerification();

        try
        {
            var verificationResponse = await kickBoxApi.VerifyWithResponse("emmaist23@gmail.com");

            if (verificationResponse is not null)
            {
                verification = ObjectMapper.Mapper.Map<KickboxVerification>(verificationResponse);

                if (!verification.Success)
                {
                    // request did not go through probably due to insufficient balance
                    verification.SupportingMessage = INSUFFICIENT_BALANCE;
                }
                else
                {
                    /* since verification.Result can have multiple values,
                    i am adding verification.SupportingMessage to provide more 
                    clarity in determining a valid email */

                    if (verification.Result.ToLower() == RISKY)
                        verification.SupportingMessage = RISKY;

                    else if (verification.Result.ToLower() == UNDELIVERABLE)
                        verification.SupportingMessage = UNDELIVERABLE;

                    else if (verification.Result.ToLower() == DELIVERABLE && verification.Reason.ToLower() == ACCEPTED_EMAIL)
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
        throw new NotImplementedException();
    }

    Task<GenericResponse> IVerificationService.IsVerified(string channel)
    {
        return Task.FromResult((GenericResponse)this.IsVerified(channel).Result);
    }
}