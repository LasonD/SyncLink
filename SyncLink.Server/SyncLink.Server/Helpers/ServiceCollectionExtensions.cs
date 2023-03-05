using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SyncLink.Infrastructure.Data.Context;
using SyncLink.Infrastructure.Data.Models.Identity;

namespace SyncLink.Server.Helpers;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfiguredIdentity(this IServiceCollection services, ConfigurationManager config)
    {
        services.AddIdentityCore<SyncLinkIdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<SyncLinkDbContext>();

        var issuer = config.GetRequiredConfig("JwtSettings:Issuer");
        var audience = config.GetRequiredConfig("JwtSettings:Audience");
        var key = config.GetRequiredConfig("JwtSettings:Key");

        services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            });

        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
        });

        return services;
    }
        
    public static IServiceCollection AddConfiguredDbContext(this IServiceCollection services, ConfigurationManager config)
    {
        var connectionString = config.GetConnectionString("SyncLinkDbContextConnection") ?? throw new InvalidOperationException("Connection string 'SyncLinkDbContextConnection' not found.");

        services.AddDbContext<SyncLinkDbContext>(options => options.UseSqlServer(connectionString));

        return services;
    }

    private static string GetRequiredConfig(this IConfiguration config, string key)
    {
        var value = config[key] ?? throw new InvalidOperationException($"Configuration '{key}' not found.");

        return value;
    }
}