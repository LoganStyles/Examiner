using Microsoft.Extensions.Configuration;

namespace Examiner.Application.Authentication;

public static class ConfigurationHelper
{
    public static IConfiguration? config;
    public static void Initialize(IConfiguration configuration)
    {
        config = configuration;
    }
}