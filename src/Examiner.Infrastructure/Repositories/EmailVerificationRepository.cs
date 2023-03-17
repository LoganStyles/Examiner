using Examiner.Domain.Entities.Notifications.Emails;
using Examiner.Infrastructure.Repositories.Base;
using Examiner.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Examiner.Infrastructure.Repositories;

public class EmailVerificationRepository<T> : BaseRepository<EmailVerification>, IEmailVerificationRepository
{
    /// <summary>
    /// EmailVerificationRepository Constructor
    /// </summary>
    /// <param name="dbContext"> The database context</param>
    public EmailVerificationRepository(DbContext context) : base(context) { }
}