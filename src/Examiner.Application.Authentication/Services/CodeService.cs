using Examiner.Application.Authentication.Interfaces;
using Examiner.Domain.Dtos;
using Examiner.Domain.Dtos.Authentication;
using Examiner.Common;
using Examiner.Infrastructure.UnitOfWork.Interfaces;
using Microsoft.Extensions.Logging;

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
    /// Fetches a verification code that does not exist or exists but has expired
    /// </summary>
    /// <returns>An GenericResponse object indicating the success or failure of an attempt to send a verification code to a user</returns>

    public async Task<CodeGenerationResponse> GetCode()
    {

        CodeGenerationResponse resultResponse = new CodeGenerationResponse(
            false,$"{AppMessages.CODE_GENERATION} {AppMessages.FAILED}");

        var verificationCode = await Generate();
        if (!string.IsNullOrWhiteSpace(verificationCode))
        {
            // check if a code exists whose expired value is false
            var existingCodeList = await _unitOfWork.CodeVerificationRepository.Get(u => u.Code.Equals(verificationCode) && u.Expired == false, null, "", null, null);
            if (existingCodeList.Count() > 0)
                await GetCode();

            resultResponse.ResultMessage = $"{AppMessages.CODE_GENERATION} {AppMessages.SUCCESSFUL}";
            resultResponse.Success = true;
            resultResponse.Code = verificationCode;
        }
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