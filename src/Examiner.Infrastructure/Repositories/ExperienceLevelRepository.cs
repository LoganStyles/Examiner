using Examiner.Domain.Entities.Content;
using Examiner.Infrastructure.Contexts;
using Examiner.Infrastructure.Repositories.Interfaces;

namespace Examiner.Infrastructure.Repositories;

public class ExperienceLevelRepository<T> : BaseRepository<ExperienceLevel>, IExperienceLevelRepository
{
    /// <summary>
    /// ExperienceLevelRepository Constructor
    /// </summary>
    /// <param name="context"> The database context</param>
    public ExperienceLevelRepository(ExaminerContext context) : base(context)
    {

    }
}