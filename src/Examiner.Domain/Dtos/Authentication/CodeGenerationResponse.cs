namespace Examiner.Domain.Dtos.Authentication;

/// <summary>
/// Specifies the result of a code generation request
/// </summary>
public class CodeGenerationResponse : GenericResponse
{
    public CodeGenerationResponse() : base(false, null)
    {

    }

    public CodeGenerationResponse(bool success, string? message) : base(success, message)
    {

    }

    public string? Code { get; set; }
}