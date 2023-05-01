using Examiner.Domain.Entities.Content;
using Microsoft.Extensions.Logging;
using Examiner.Application.Content.Interfaces;
using Examiner.Infrastructure.UnitOfWork.Interfaces;

namespace Examiner.Application.Content.Services;

/// <summary>
/// Implements contract for fetching subject categories
/// </summary>
public class SubjectCategoryService : ISubjectCategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SubjectCategoryService> _logger;

    public SubjectCategoryService(IUnitOfWork unitOfWork, ILogger<SubjectCategoryService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <summary>
    /// Describes contract for fetching user details
    /// </summary>
    public async Task<IEnumerable<SubjectCategory>?> GetAllAsync()
    {
        try
        {
            var subjectCategoryList = await _unitOfWork.SubjectCategoryRepository.Get(null, null, "", null, null);
            if (subjectCategoryList is not null)
                return subjectCategoryList;
            else
                return null;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error fetching subject categories - ", ex.Message);
            throw;
        }
    }

    public async Task<SubjectCategory?> GetByIdAsync(int Id)
    {
        try
        {
            var existingSubjectCategory = await _unitOfWork.SubjectCategoryRepository.GetByIdAsync(Id);
            if (existingSubjectCategory is not null)
                return existingSubjectCategory;
            else
                return null;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error fetching subject categories - ", ex.Message);
            throw;
        }
    }
}