using Examiner.Domain.Entities.Content;
using Examiner.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Examiner.Infrastructure.Repositories;

public class CountryRepository<T> : BaseRepository<Country>, ICountryRepository
{
    /// <summary>
    /// Country Repository Constructor
    /// </summary>
    /// <param name="dbContext"> The database context</param>
    public CountryRepository(DbContext context) : base(context) { }
}