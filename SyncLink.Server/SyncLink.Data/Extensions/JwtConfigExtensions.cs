using Microsoft.Extensions.Configuration;

namespace SyncLink.Infrastructure.Extensions;

public static class JwtConfigExtensions
{
    private const string TokenGenerationKey = "JwtSettings:Key";
    private const string IssuerKey = "JwtSettings:Issuer";
    private const string AudienceKey = "JwtSettings:Audience";
    private const string DurationMinutesKey = "JwtSettings:DurationMinutes";

    public static string GetTokenGenerationKey(this IConfiguration config) => config.GetRequiredConfig(TokenGenerationKey);

    public static int GetTokenDurationMinutes(this IConfiguration config) => int.Parse(config.GetRequiredConfig(DurationMinutesKey));

    public static string GetIssuer(this IConfiguration config) => config.GetRequiredConfig(IssuerKey);

    public static string GetAudience(this IConfiguration config) => config.GetRequiredConfig(AudienceKey);

    private static string GetRequiredConfig(this IConfiguration config, string key)
    {
        var value = config[key] ?? throw new InvalidOperationException($"Configuration '{key}' not found.");

        return value;
    }
}