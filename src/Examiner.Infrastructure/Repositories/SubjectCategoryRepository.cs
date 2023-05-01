using Examiner.Domain.Entities.Content;
using Examiner.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Examiner.Infrastructure.Repositories;

public class SubjectCategoryRepository<T> : BaseRepository<SubjectCategory>, ISubjectCategoryRepository{
    /// <summary>
    /// SubjectCategory Repository Constructor
    /// </summary>
    /// <param name="dbContext"> The database context</param>
    public SubjectCategoryRepository(DbContext context) : base(context) { }
}