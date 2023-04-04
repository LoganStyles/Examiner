using System.ComponentModel.DataAnnotations;
using Examiner.Domain.Entities;

namespace Examiner.Domain.Entities.Notifications.Emails;

/// <summary>
/// Entity representing the response from a kickbox request 
/// </summary>
public class KickboxVerification : BaseEntity
{

    [StringLength(200)]
    public string? Message { get; set; }

    public int Code { get; set; }

    [StringLength(20)]
    public string? Result { get; set; }// be guided while using this property, since the name clashes with a property of the GenericResponse object

    [StringLength(20)]
    public string? Reason { get; set; }

    public bool Role { get; set; }

    public bool Free { get; set; }

    public bool Disposable { get; set; }

    public bool AcceptAll { get; set; }

    [StringLength(100)]
    public string? DidYouMean { get; set; }

    public double Sendex { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [StringLength(100)]
    public string? User { get; set; }

    [StringLength(100)]
    public string? Domain { get; set; }

    public bool Success { get; set; } // be guided while using this property, since the name clashes with the GenericResponse object

    // [EmailAddress]
    // public string? EmailReturned { get; set; }

    [StringLength(200)]
    public string? SupportingMessage { get; set; }
    public bool IsValidEmail { get; set; }

}