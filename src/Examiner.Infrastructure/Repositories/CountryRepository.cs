using Examiner.Domain.Entities.Content;
using Examiner.Infrastructure.Contexts;
using Examiner.Infrastructure.Repositories.Interfaces;

namespace Examiner.Infrastructure.Repositories;

public class CountryRepository<T> : BaseRepository<Country>, ICountryRepository
{
    /// <summary>
    /// Country Repository Constructor
    /// </summary>
    /// <param name="context"> The database context</param>
    public CountryRepository(ExaminerContext context) : base(context) { }
}