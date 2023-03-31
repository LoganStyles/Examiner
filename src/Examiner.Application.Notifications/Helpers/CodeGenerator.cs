namespace Examiner.Application.Notifications.Helpers;

/// <summary>
/// Class for generating verification codes
/// This is static for now but I may review later
/// </summary>
public static class CodeGenerator
{
    /// <summary>
    /// Generates a verification code
    /// </summary>
    /// <returns>A six digit string</returns>
    public static async Task<string> GenerateCode()
    {

        string newRandom = new Random().Next(0, 1000000).ToString("D6");
        if (newRandom.Distinct<char>().Count<char>() == 1)
            newRandom = await GenerateCode();
        return newRandom;
    }
}