using Examiner.Domain.Entities.Content;
using Examiner.Infrastructure.Contexts;
using Examiner.Infrastructure.Repositories.Interfaces;

namespace Examiner.Infrastructure.Repositories;

public class StateRepository<T> : BaseRepository<State>, IStateRepository
{
    /// <summary>
    /// State Repository Constructor
    /// </summary>
    /// <param name="context"> The database context</param>
    public StateRepository(ExaminerContext context) : base(context) { }
}