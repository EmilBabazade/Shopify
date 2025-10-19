using Microsoft.Extensions.Configuration;

namespace CommonExtensions;

public static class ConfigurationExtensions
{
    public static string GetRequiredValue(this IConfiguration config, string key)
    {
        var value = config[key];
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException($"Missing required configuration key: '{key}'.");
        return value;
    }
}
