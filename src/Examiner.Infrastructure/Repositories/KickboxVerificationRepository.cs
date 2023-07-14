using Examiner.Domain.Entities.Notifications.Emails;
using Examiner.Infrastructure.Contexts;
using Examiner.Infrastructure.Repositories.Interfaces;

namespace Examiner.Infrastructure.Repositories;

public class KickboxVerificationRepository<T> : BaseRepository<KickboxVerification>, IKickboxVerificationRepository
{
    /// <summary>
    /// EmailVerificationRepository Constructor
    /// </summary>
    /// <param name="context"> The database context</param>
    public KickboxVerificationRepository(ExaminerContext context) : base(context) { }
}