namespace Examiner.Domain.Dtos.Content;

/// <summary>
/// Specifies the result of a content request
/// </summary>

public class ContentResponse : GenericResponse
{
    public ContentResponse() : base(false, null)
    {

    }

    public ContentResponse(bool success, string? message) : base(success, message)
    {

    }

    public List<ContentDto>? Contents { get; set; }
}