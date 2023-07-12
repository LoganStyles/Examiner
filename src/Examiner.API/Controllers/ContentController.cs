using Examiner.Application.Content.Interfaces;
using Examiner.Common;
using Examiner.Domain.Dtos.Content;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Examiner.API.Controllers;

/// <summary>
/// Provides endpoints for processing tutor subject content
/// </summary>
[ApiController]
[Authorize]
[Route("api/[controller]")]

public class ContentController : ControllerBase
{

    private readonly ISubjectCategoryService _subjectCategoryService;
    private readonly ISubjectService _subjectService;
    private readonly ICountryService _countryService;
    private readonly IStateService _stateService;

    public ContentController(ISubjectCategoryService subjectCategoryService,
    ISubjectService subjectService,
    ICountryService countryService,
    IStateService stateService)
    {
        _subjectCategoryService = subjectCategoryService;
        _subjectService = subjectService;
        _stateService = stateService;
        _countryService = countryService;
    }

    /// <summary>
    /// Fetches all subject-categories.
    /// </summary>
    /// <returns>A content Response indicating success or failure of the request as well as any existing subject-categories</returns>
    [HttpGet("subject-categories")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContentResponse>> GetAllSubjectCategoriesAsync()
    {

        var response = new ContentResponse(false, $"{AppMessages.SUBJECT_CATEGORY} {AppMessages.NOT_EXIST}");
        var categories = await _subjectCategoryService.GetAllAsync();
        if (categories is not null && categories.Count() > 0)
        {
            response.Success = true;
            response.ResultMessage = $"{AppMessages.SUBJECT_CATEGORY} {AppMessages.EXISTS}";
            response.Contents = new List<ContentDto>();
            foreach (var category in categories)
            {
                response.Contents.Add(new ContentDto() { Id = category.Id, Title = category.Title });
            }
            return Ok(response);
        }
        else
            return NotFound(response);

    }

    /// <summary>
    /// Fetches all subjects for a specific subject-category.
    /// </summary>
    /// <returns>A content Response indicating success or failure of the request as well as any existing subjects for the specified subject-category</returns>
    [HttpPost("subjects")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContentResponse>> GetAllSubjectsByCategoryAsync([FromBody] SubjectRequest request)
    {

        var response = new ContentResponse(false, $"{AppMessages.SUBJECT} {AppMessages.NOT_EXIST}");
        var subjects = await _subjectService.GetAllByCategoryAsync(request.categoryId);
        if (subjects is not null && subjects.Count() > 0)
        {
            response.Success = true;
            response.ResultMessage = $"{AppMessages.SUBJECT} {AppMessages.EXISTS}";
            response.Contents = new List<ContentDto>();
            foreach (var subject in subjects)
            {
                response.Contents.Add(new ContentDto() { Id = subject.Id, Title = subject.Title });
            }
            return Ok(response);
        }
        else
            return NotFound(response);

    }

    /// <summary>
    /// Fetches all existing countries.
    /// </summary>
    /// <returns>A content Response indicating success or failure of the request as well as any existing countries</returns>
    [HttpGet("countries")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContentResponse>> GetAllCountriesAsync()
    {

        var response = new ContentResponse(false, $"{AppMessages.COUNTRY} {AppMessages.NOT_EXIST}");
        var countries = await _countryService.GetAllAsync();
        if (countries is not null && countries.Count() > 0)
        {
            response.Success = true;
            response.ResultMessage = $"{AppMessages.COUNTRY} {AppMessages.EXISTS}";
            response.Contents = new List<ContentDto>();
            foreach (var country in countries)
            {
                response.Contents.Add(new ContentDto() { Id = country.Id, Title = country.Title });
            }
            return Ok(response);
        }
        else
            return NotFound(response);

    }

    /// <summary>
    /// Fetches all states for a specific country.
    /// </summary>
    /// <returns>A content Response indicating success or failure of the request as well as any existing states for the specified country</returns>
    [HttpPost("states")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContentResponse>> GetAllStatesAsync([FromBody] StateRequest request)
    {

        var response = new ContentResponse(false, $"{AppMessages.STATE} {AppMessages.NOT_EXIST}");
        var states = await _stateService.GetAllByCategoryAsync(request.countryId);
        if (states is not null && states.Count() > 0)
        {
            response.Success = true;
            response.ResultMessage = $"{AppMessages.STATE} {AppMessages.EXISTS}";
            response.Contents = new List<ContentDto>();
            foreach (var state in states)
            {
                response.Contents.Add(new ContentDto() { Id = state.Id, Title = state.Title });
            }
            return Ok(response);
        }
        else
            return NotFound(response);

    }


}