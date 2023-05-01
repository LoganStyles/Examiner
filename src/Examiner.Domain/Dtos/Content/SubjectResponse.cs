namespace Examiner.Domain.Dtos.Content;

/// <summary>
/// Specifies the result of a subject  request
/// </summary>

public class SubjectResponse : GenericResponse
{
    public SubjectResponse() : base(false, null)
    {

    }

    public SubjectResponse(bool success, string? message) : base(success, message)
    {

    }

    public List<SubjectDto>? Subjects { get; set; }
}