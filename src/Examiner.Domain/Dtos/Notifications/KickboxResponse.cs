using System.ComponentModel.DataAnnotations;

namespace Examiner.Domain.Dtos.Notifications;

/// <summary>
/// Describes a kickbox response object
/// </summary>
public class KickboxResponse
{
    public string Message { get; set; } = null!;

    public int Code { get; set; }

    public string Result { get; set; } = null!;

    public string Reason { get; set; } = null!;

    public bool Role { get; set; }

    public bool Free { get; set; }

    public bool Disposable { get; set; }

    public bool AcceptAll { get; set; }

    public string DidYouMean { get; set; } = null!;

    public double Sendex { get; set; }

    [Required(ErrorMessage = "{0} is a mandatory field")]
    [EmailAddress]
    public string Email { get; set; } = null!;

    public string User { get; set; } = null!;

    public string Domain { get; set; } = null!;

    public bool Success { get; set; }
}