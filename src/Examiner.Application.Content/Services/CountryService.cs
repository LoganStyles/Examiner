using Examiner.Domain.Entities.Content;
using Microsoft.Extensions.Logging;
using Examiner.Application.Content.Interfaces;
using Examiner.Infrastructure.UnitOfWork.Interfaces;

namespace Examiner.Application.Content.Services;

/// <summary>
/// Implements contract for fetching countries
/// </summary>
public class CountryService : ICountryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CountryService> _logger;

    public CountryService(IUnitOfWork unitOfWork, ILogger<CountryService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <summary>
    /// Describes contract for fetching user details
    /// </summary>
    public async Task<IEnumerable<Country>?> GetAllAsync()
    {
        try
        {
            var countryList = await _unitOfWork.CountryRepository.Get(null, null, "", null, null);
            if (countryList is not null)
                return countryList;
            else
                return null;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error fetching countries - ", ex.Message);
            throw;
        }
    }

    public async Task<Country?> GetByIdAsync(int Id)
    {
        try
        {
            var existingCountry = await _unitOfWork.CountryRepository.GetByIdAsync(Id);
            if (existingCountry is not null)
                return existingCountry;
            else
                return null;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error fetching countries - ", ex.Message);
            throw;
        }
    }
}