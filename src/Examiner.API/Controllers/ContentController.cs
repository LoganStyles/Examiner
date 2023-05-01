using Examiner.Application.Content.Interfaces;
using Examiner.Authentication.Domain.Mappings;
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

    public ContentController(ISubjectCategoryService subjectCategoryService,
    ISubjectService subjectService)
    {
        _subjectCategoryService = subjectCategoryService;
        _subjectService = subjectService;
    }

    [HttpGet("subject-categories")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SubjectCategoryResponse>> GetAllSubjectCategoriesAsync()
    {

        var response = new SubjectCategoryResponse(false, $"{AppMessages.SUBJECT_CATEGORY} {AppMessages.NOT_EXIST}");
        var categories = await _subjectCategoryService.GetAllAsync();
        if (categories is not null && categories.Count() >0)
        {
            response.Success = true;
            response.ResultMessage = $"{AppMessages.SUBJECT_CATEGORY} {AppMessages.EXISTS}";
            response.SubjectCategories = new List<SubjectCategoryDto>();
            foreach (var category in categories)
            {
                response.SubjectCategories.Add(ObjectMapper.Mapper.Map<SubjectCategoryDto>(category));
            }
            return Ok(response);
        }
        else
            return NotFound(response);

    }

    [HttpPost("subjects")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SubjectResponse>> GetAllSubjectsByCategoryAsync([FromBody] SubjectRequest request)
    {

        var response = new SubjectResponse(false, $"{AppMessages.SUBJECT} {AppMessages.NOT_EXIST}");
        var subjects = await _subjectService.GetAllByCategoryAsync(request.categoryId);
        if (subjects is not null && subjects.Count() >0)
        {
            response.Success = true;
            response.ResultMessage = $"{AppMessages.SUBJECT} {AppMessages.EXISTS}";
            response.Subjects = new List<SubjectDto>();
            foreach (var subject in subjects)
            {
                response.Subjects.Add(ObjectMapper.Mapper.Map<SubjectDto>(subject));
            }
            return Ok(response);
        }
        else
            return NotFound(response);

    }


}