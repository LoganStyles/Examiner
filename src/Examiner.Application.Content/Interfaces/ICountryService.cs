using Examiner.Domain.Entities.Content;

namespace Examiner.Application.Content.Interfaces;

/// <summary>
/// Describes contract for countries
/// </summary>
public interface ICountryService
{
    Task<Country?> GetByIdAsync(int Id);
    Task<IEnumerable<Country>?> GetAllAsync();
}