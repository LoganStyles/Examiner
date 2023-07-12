using Examiner.Domain.Entities.Content;

namespace Examiner.Application.Content.Interfaces;

/// <summary>
/// Describes contract for states
/// </summary>
public interface IStateService
{
    Task<IEnumerable<State>?> GetAllByCategoryAsync(int countryId);
}