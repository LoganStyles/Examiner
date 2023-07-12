using Examiner.Domain.Entities.Content;
using Examiner.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Examiner.Infrastructure.Repositories;

public class StateRepository<T> : BaseRepository<State>, IStateRepository
{
    /// <summary>
    /// State Repository Constructor
    /// </summary>
    /// <param name="dbContext"> The database context</param>
    public StateRepository(DbContext context) : base(context) { }
}