using Examiner.Common;
using Examiner.Domain.Dtos.Authentication;
using Examiner.Domain.Entities.Authentication;
using Examiner.Domain.Dtos;

namespace Examiner.Tests.MockData;

public static class CodeVerificationMock
{
    public static Task<string> EmptyGeneratedCode()
    {
        return null!;
    }

    public static Task<IEnumerable<CodeVerification>> GetEmptyListOfExistingCodes()
    {
        return Task.FromResult((new List<CodeVerification>()).AsEnumerable());
    }

    public static IEnumerable<CodeVerification> GetExistingCodeVerificationHavingExpiredCode()
    {
        return new List<CodeVerification>
        {

            new CodeVerification{
                Code = "000000",
                IsSent = true,
                Attempts = 0,
                Expired = true
            }
        };
    }
    public static IEnumerable<CodeVerification> GetExistingCodeVerificationHavingExpiredCodeAndExpiredAttempts()
    {
        return new List<CodeVerification>
        {

            new CodeVerification{
                Code = "000000",
                IsSent = true,
                Attempts = 3,
                Expired = false
            }
        };
    }

    public static IEnumerable<CodeVerification> GetExistingCodeVerificationHavingValidCode()
    {
        return new List<CodeVerification>
        {

            new CodeVerification{
                Code = "000000",
                IsSent = true,
                Attempts = 0,
                Expired = false
            }
        };
    }

    public static Task<CodeGenerationResponse> GetSuccessfulCodeGenerationResponse()
    {
        return Task.FromResult(new CodeGenerationResponse
        {
            Success = true,
            ResultMessage = $"{AppMessages.CODE_CREATION} {AppMessages.SUCCESSFUL}",
            Code = "901290"
        });
    }
    public static Task<GenericResponse> GetExpiredCodeVerificationResponse()
    {
        return Task.FromResult(new GenericResponse(false, $"{AppMessages.CODE_SUPPLIED} {AppMessages.EXPIRED}"));
    }
    public static Task<GenericResponse> GetNotMatchingCodeVerificationResponse()
    {
        return Task.FromResult(new GenericResponse(false, $"{AppMessages.CODE_SUPPLIED} {AppMessages.NOT_EXIST}"));
    }
    public static Task<GenericResponse> GetMatchingCodeVerificationResponse()
    {
        return Task.FromResult(new GenericResponse(true, $"{AppMessages.CODE_VERIFICATION} {AppMessages.SUCCESSFUL}"));
    }
    public static Task<GenericResponse> ResendVerificationCodeResponse()
    {
        return Task.FromResult(new GenericResponse(true, $"{AppMessages.CODE_RESEND} {AppMessages.SUCCESSFUL}"));
    }
}