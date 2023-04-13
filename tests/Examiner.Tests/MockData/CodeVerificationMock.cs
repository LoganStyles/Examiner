using Examiner.Common;
using Examiner.Domain.Dtos.Authentication;
using Examiner.Domain.Entities.Authentication;

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

    public static Task<CodeGenerationResponse> GetSuccessfulCodeGenerationResponse()
    {
        return Task.FromResult(new CodeGenerationResponse
        {
            Success = true,
            ResultMessage = $"{AppMessages.CODE_GENERATION} {AppMessages.SUCCESSFUL}",
            Code = "901290"
        });
    }
}