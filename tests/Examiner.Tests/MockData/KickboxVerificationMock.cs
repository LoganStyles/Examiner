using Examiner.Domain.Entities.Notifications.Emails;

namespace Examiner.Tests.MockData;

public static class KickboxVerificationMock
{

    public static Task<IEnumerable<KickboxVerification>> GetListOfExistingVerifications()
    {
        return Task.FromResult((
            new List<KickboxVerification>{
            new KickboxVerification(){
                Email="y@gmail.com",
                Message="",
                Success=true
            }
        }).AsEnumerable());

    }

    public static Task<IEnumerable<KickboxVerification>> GetEmptyListOfExistingVerifications()
    {
        return Task.FromResult((new List<KickboxVerification>()).AsEnumerable());
    }
}