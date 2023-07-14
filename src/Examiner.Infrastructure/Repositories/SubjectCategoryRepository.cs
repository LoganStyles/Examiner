using Examiner.Domain.Entities.Content;
using Examiner.Infrastructure.Contexts;
using Examiner.Infrastructure.Repositories.Interfaces;

namespace Examiner.Infrastructure.Repositories;

public class SubjectCategoryRepository<T> : BaseRepository<SubjectCategory>, ISubjectCategoryRepository{
    /// <summary>
    /// SubjectCategory Repository Constructor
    /// </summary>
    /// <param name="context"> The database context</param>
    public SubjectCategoryRepository(ExaminerContext context) : base(context) { }
}