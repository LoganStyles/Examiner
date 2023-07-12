using Examiner.Application.Content.Interfaces;
using Examiner.Domain.Entities.Content;
using Examiner.Infrastructure.UnitOfWork.Interfaces;
using Microsoft.Extensions.Logging;

namespace Examiner.Application.Content.Services;

public class StateService : IStateService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<StateService> _logger;

    public StateService(IUnitOfWork unitOfWork, ILogger<StateService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task<IEnumerable<State>?> GetAllByCategoryAsync(int countryId)
    {
        try
        {

            var stateList = await _unitOfWork.StateRepository.Get(s => s.CountryId == countryId, null, "", null, null);
            if (stateList is not null)
            {
                return stateList;
            }
            else
                return null;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error fetching states - ", ex.Message);
            throw;
        }
    }
}