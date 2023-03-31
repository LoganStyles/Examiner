using Examiner.Domain.Entities.Notifications.Emails;
using Examiner.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Examiner.Infrastructure.Repositories;

public class KickboxVerificationRepository<T> : BaseRepository<KickboxVerification>, IKickboxVerificationRepository
{
    /// <summary>
    /// EmailVerificationRepository Constructor
    /// </summary>
    /// <param name="dbContext"> The database context</param>
    public KickboxVerificationRepository(DbContext context) : base(context) { }
}