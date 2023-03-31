namespace Examiner.Application.Notifications.Helpers;

/// <summary>
/// Provides configuration details for kickbox connection
/// </summary>
public class AppSettings
{

    public string ContentType { get; set; } = null!;
    public string KickboxKey { get; set; } = null!;
    public string KickboxUrl { get; set; } = null!;
    public string KickboxVerifyPath { get; set; } = null!;
    public string FromEmail { get; set; } = null!;
}