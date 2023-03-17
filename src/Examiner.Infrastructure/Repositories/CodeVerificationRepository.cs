using Examiner.Domain.Entities.Notifications.Emails;
using Examiner.Infrastructure.Repositories.Base;
using Examiner.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Examiner.Infrastructure.Repositories;

public class CodeVerificationRepository<T> : BaseRepository<CodeVerification>, ICodeVerificationRepository
{
    /// <summary>
    /// CodeVerificationRepository Constructor
    /// </summary>
    /// <param name="dbContext"> The database context</param>
    public CodeVerificationRepository(DbContext context) : base(context) { }
}