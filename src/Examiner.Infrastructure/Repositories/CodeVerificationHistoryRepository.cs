using Examiner.Authentication.Domain.Entities;
using Examiner.Infrastructure.Repositories.Base;
using Examiner.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Examiner.Infrastructure.Repositories;

public class CodeVerificationHistoryRepository<T> : BaseRepository<CodeVerificationHistory>, ICodeVerificationHistoryRepository
{

    /// <summary>
    /// CodeVerificationHistoryRepository Constructor
    /// </summary>
    /// <param name="dbContext"> The database context</param>
    public CodeVerificationHistoryRepository(DbContext context) : base(context)
    {
    }
}