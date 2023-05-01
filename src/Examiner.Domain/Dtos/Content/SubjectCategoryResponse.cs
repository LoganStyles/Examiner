namespace Examiner.Domain.Dtos.Content;

/// <summary>
/// Specifies the result of a subject category request
/// </summary>

public class SubjectCategoryResponse : GenericResponse
{
    public SubjectCategoryResponse() : base(false, null)
    {

    }

    public SubjectCategoryResponse(bool success, string? message) : base(success, message)
    {

    }

    public List<SubjectCategoryDto>? SubjectCategories { get; set; }
}