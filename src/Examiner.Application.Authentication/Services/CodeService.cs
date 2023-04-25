using Examiner.Application.Authentication.Interfaces;
using Examiner.Domain.Dtos;
using Examiner.Domain.Dtos.Authentication;
using Examiner.Common;
using Examiner.Infrastructure.UnitOfWork.Interfaces;
using Microsoft.Extensions.Logging;
using Examiner.Domain.Entities.Authentication;
using System.Linq.Expressions;
using Examiner.Domain.Entities.Users;

namespace Examiner.Application.Authentication.Services;

public class CodeService : ICodeService
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CodeService> _logger;


    public CodeService(IUnitOfWork unitOfWork, ILogger<CodeService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <summary>
    /// Creates a verification code that does not exist or exists but has expired
    /// </summary>
    /// <returns>An GenericResponse object indicating the success or failure of an attempt to send a verification code to a user</returns>

    public async Task<CodeGenerationResponse> CreateCode()
    {

        CodeGenerationResponse resultResponse = new CodeGenerationResponse(
            false, $"{AppMessages.CODE_CREATION} {AppMessages.FAILED}");

        var verificationCode = await Generate();
        if (!string.IsNullOrWhiteSpace(verificationCode))
        {
            // check if a code exists whose expired value is false
            var existingCodeList = await _unitOfWork.CodeVerificationRepository.Get(u => u.Code.Equals(verificationCode) && u.Expired == false, null, "", null, null);
            if (existingCodeList.Count() > 0)
                await CreateCode();

            resultResponse.ResultMessage = $"{AppMessages.CODE_CREATION} {AppMessages.SUCCESSFUL}";
            resultResponse.Success = true;
            resultResponse.Code = verificationCode;
        }
        return resultResponse;
    }

    /// <summary>
    /// Fetches a code if it exists
    /// </summary>
    /// <param name="code">The code to fetch</param>
    /// <returns>An object holding data indicating the success or failure of fetching the users</returns>
    public async Task<CodeVerification?> GetCode(string code)
    {
        try
        {
            Func<IQueryable<CodeVerification>, IOrderedQueryable<CodeVerification>>? orderBy = null;
            Expression<Func<CodeVerification, bool>>? filter = (c => c.Code == code);

            var codeList = await _unitOfWork.CodeVerificationRepository.Get(filter, orderBy, string.Empty, null, null);
            if (codeList.Count() > 0)
            {
                return codeList.FirstOrDefault();
            }
            return null;

        }
        catch (Exception ex)
        {
            _logger.LogError("Error fetching code - ", ex.Message);
            throw;
        }
    }
   

    /// <summary>
    /// Verifies a code
    /// </summary>
    /// <returns>An GenericResponse object indicating the success or failure of an attempt to send a verification code to a user</returns>
    public async Task<GenericResponse> VerifyCode(User user, string suppliedCode)
    {

        GenericResponse resultResponse = new GenericResponse(
            false, $"{AppMessages.CODE_VERIFICATION} {AppMessages.FAILED}");

        if (user is null)
        {
            throw new InvalidOperationException($"{AppMessages.INVALID_REQUEST} :{AppMessages.USER} {AppMessages.NOT_EXIST}");
        }

        // when a user who has not been sent a code requests for verification (this should not happen)
        if (user.CodeVerification is null)
        {
            throw new InvalidOperationException($"{AppMessages.INVALID_REQUEST} :{AppMessages.CODE_SUPPLIED} {AppMessages.NOT_EXIST}");
        }

        user.CodeVerification.Attempts += 1;
        var suppliedCodeResult = await this.GetCode(suppliedCode);

        if (suppliedCodeResult is null || suppliedCodeResult.Code != user.CodeVerification.Code)
            resultResponse.ResultMessage = $"{AppMessages.CODE_SUPPLIED} {AppMessages.NOT_EXIST}";

        else if (user.CodeVerification.Expired || (DateTime.Now - user.CodeVerification.CreatedDate).Seconds >= user.CodeVerification.ExpiresIn)
        {
            user.CodeVerification.Expired = true;
            resultResponse.ResultMessage = $"{AppMessages.CODE_SUPPLIED} {AppMessages.EXPIRED}";
        }
        else if (user.CodeVerification.Code == suppliedCodeResult.Code)
        {
            user.CodeVerification.HasVerified = true;
            user.CodeVerification.VerifiedAt = DateTime.Now;
            user.IsActive = true;

            resultResponse.Success = true;
            resultResponse.ResultMessage = $"{AppMessages.CODE_VERIFICATION} {AppMessages.SUCCESSFUL}";
        }
        else
        {
            if (user.CodeVerification.Attempts >= 3)
            {
                user.CodeVerification.Expired = true;
                resultResponse.ResultMessage = $"{AppMessages.CODE_SUPPLIED} {AppMessages.EXPIRED}";
            }
        }

        await _unitOfWork.UserRepository.Update(user);
        await _unitOfWork.CompleteAsync();

        return resultResponse;
    }

    /// <summary>
    /// Generates a verification code
    /// </summary>
    /// <returns>A six digit string</returns>
    private async Task<string> Generate()
    {

        string newRandom = new Random().Next(0, 1000000).ToString("D6");
        if (newRandom.Distinct<char>().Count<char>() == 1)
            newRandom = await Generate();
        return newRandom;
    }

    public Task<GenericResponse> IsVerified(string item)
    {
        throw new NotImplementedException();
    }

    public Task<GenericResponse> IsValid(string item)
    {
        throw new NotImplementedException();
    }

}