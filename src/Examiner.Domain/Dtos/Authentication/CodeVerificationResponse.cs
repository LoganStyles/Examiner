namespace Examiner.Domain.Dtos.Authentication;

/// <summary>
/// Specifies the result of a code verification request
/// </summary>
public class CodeVerificationResponse : GenericResponse
{
    public CodeVerificationResponse() : base(false, null)
    {

    }

    public CodeVerificationResponse(bool success, string? message) : base(success, message)
    {

    }

    public string? Code { get; set; }
}