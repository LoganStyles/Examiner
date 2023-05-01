using Examiner.Domain.Entities.Content;

namespace Examiner.Application.Content.Interfaces;

/// <summary>
/// Describes contract for fetching subjects
/// </summary>
public interface ISubjectService
{
    Task<IEnumerable<Subject>?> GetAllByCategoryAsync(int categoryId);
}