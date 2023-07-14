using Examiner.Domain.Entities.Content;
using Examiner.Infrastructure.Contexts;
using Examiner.Infrastructure.Repositories.Interfaces;

namespace Examiner.Infrastructure.Repositories;

public class SubjectRepository<T> : BaseRepository<Subject>, ISubjectRepository
{

    /// <summary>
    /// SubjectRepository Constructor
    /// </summary>
    /// <param name="context"> The database context</param>
    public SubjectRepository(ExaminerContext context) : base(context) { }
}