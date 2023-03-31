using Examiner.Domain.Dtos.Notifications;

namespace Examiner.Domain.Dtos;

/// <summary>
/// Implements a generic response object
/// </summary>
public class GenericResponse
{
    public bool Success { get; set; }
    public string? ResultMessage { get; set; }

    public GenericResponse(bool success, string? message)
    {
        Success = success;
        ResultMessage = message;
    }

    public static GenericResponse Result(bool success, string? message) => new GenericResponse(success, message);

    // public static explicit operator GenericResponse(Task<KickboxResponse> v)
    // {
    //     throw new NotImplementedException();
    // }
}