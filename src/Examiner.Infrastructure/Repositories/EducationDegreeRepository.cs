using Examiner.Domain.Entities.Content;
using Examiner.Infrastructure.Contexts;
using Examiner.Infrastructure.Repositories.Interfaces;

namespace Examiner.Infrastructure.Repositories;

public class EducationDegreeRepository<T> : BaseRepository<EducationDegree>, IEducationDegreeRepository
{
    /// <summary>
    /// EducationDegreeRepository Constructor
    /// </summary>
    /// <param name="context"> The database context</param>
    public EducationDegreeRepository(ExaminerContext context) : base(context)
    {

    }
}