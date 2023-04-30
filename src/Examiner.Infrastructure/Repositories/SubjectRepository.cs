using Examiner.Domain.Entities.Content;
using Examiner.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Examiner.Infrastructure.Repositories;

public class SubjectRepository<T> : BaseRepository<Subject>, ISubjectRepository
{

    /// <summary>
    /// SubjectRepository Constructor
    /// </summary>
    /// <param name="dbContext"> The database context</param>
    public SubjectRepository(DbContext context) : base(context) { }
}