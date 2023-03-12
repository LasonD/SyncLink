using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SyncLink.Application.Contracts.Data;
using SyncLink.Application.Mapping;
using SyncLink.Application.UseCases.Auth.Register;
using SyncLink.Infrastructure.Data.Context;
using SyncLink.Infrastructure.Data.Models.Identity;
using SyncLink.Infrastructure.Data.Repositories;
using SyncLink.Infrastructure.Extensions;
using SyncLink.Server.Filters;
using SyncLink.Server.Middleware;

namespace SyncLink.Server.Helpers;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfiguredIdentity(this IServiceCollection services, ConfigurationManager config)
    {
        services.AddIdentityCore<SyncLinkIdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<SyncLinkDbContext>();

        var issuer = config.GetIssuer();
        var audience = config.GetAudience();
        var key = config.GetTokenGenerationKey();

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

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program), typeof(ApplicationProfile), typeof(SyncLinkDbContext));
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(RegisterHandler).Assembly));
        services.AddScoped<ErrorHandler>();

        return services;
    }

    public static IServiceCollection AddApiWithSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddControllers(options => options.Filters.Add<ValidateModelStateAttribute>());
        services.AddSignalR();
        services.AddSwaggerGen();

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IAuthRepository, AuthRepository>();

        return services;
    }
}