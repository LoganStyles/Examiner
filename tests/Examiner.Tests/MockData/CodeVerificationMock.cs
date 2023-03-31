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
}