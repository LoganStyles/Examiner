using Examiner.Domain.Entities.Authentication;
using Examiner.Infrastructure.Contexts;
using Examiner.Infrastructure.Repositories.Interfaces;

namespace Examiner.Infrastructure.Repositories;

public class CodeVerificationRepository<T> : BaseRepository<CodeVerification>, ICodeVerificationRepository
{
    /// <summary>
    /// CodeVerificationRepository Constructor
    /// </summary>
    /// <param name="context"> The database context</param>
    public CodeVerificationRepository(ExaminerContext context) : base(context) { }
}