using Examiner.Application.Content.Interfaces;
using Examiner.Domain.Entities.Content;
using Examiner.Infrastructure.UnitOfWork.Interfaces;
using Microsoft.Extensions.Logging;

namespace Examiner.Application.Content.Services;

public class SubjectService : ISubjectService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SubjectService> _logger;

    public SubjectService(IUnitOfWork unitOfWork, ILogger<SubjectService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    // public Task<IEnumerable<Subject>?> GetAllAsync()
    // {
    //     throw new NotImplementedException();
    // }

    // public Task<Subject?> GetByIdAsync(int Id)
    // {
    //     throw new NotImplementedException();
    // }

    public async Task<IEnumerable<Subject>?> GetAllByCategoryAsync(int categoryId)
    {
        try
        {

            var subjectList = await _unitOfWork.SubjectRepository.Get(s => s.SubjectCategoryId == categoryId, null, "", null, null);
            if (subjectList is not null)
            {
                return subjectList;
            }
            else
                return null;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error fetching subjects - ", ex.Message);
            throw;
        }
    }
}