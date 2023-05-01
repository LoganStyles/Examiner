using Examiner.Domain.Entities;
using Examiner.Domain.Entities.Content;

namespace Examiner.Application.Content.Interfaces;

/// <summary>
/// Describes contract for contents
/// </summary>
public interface ISubjectCategoryService
{
    Task<SubjectCategory?> GetByIdAsync(int Id);
    Task<IEnumerable<SubjectCategory>?> GetAllAsync();
}