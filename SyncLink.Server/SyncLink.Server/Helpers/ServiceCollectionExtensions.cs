using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.RealTime;
using SyncLink.Application.Domain;
using SyncLink.Application.Domain.Features;
using SyncLink.Application.Dtos;
using SyncLink.Application.Mapping;
using SyncLink.Application.UseCases.Commands.Auth.Register;
using SyncLink.Application.UseCases.Queries.GetById.Group;
using SyncLink.Application.UseCases.Queries.GetById.Whiteboard;
using SyncLink.Infrastructure.Data.Context;
using SyncLink.Infrastructure.Data.Models.Identity;
using SyncLink.Infrastructure.Data.Repositories;
using SyncLink.Infrastructure.Extensions;
using SyncLink.Server.Common;
using SyncLink.Server.Filters;
using SyncLink.Server.Middleware;
using SyncLink.Server.SignalR;

namespace SyncLink.Server.Helpers;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfiguredIdentity(this IServiceCollection services, ConfigurationManager config)
    {
        services.AddIdentityCore<SyncLinkIdentityUser>(options =>
            {
                options.Password = new PasswordOptions
                {
                    RequireDigit = false,
                    RequiredLength = 4,
                    RequireLowercase = false,
                    RequireUppercase = false,
                    RequireNonAlphanumeric = false,
                };
            })
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
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        if (!accessToken.IsNullOrEmpty())
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
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
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Register).Assembly));
        services.AddScoped<ErrorHandler>();

        return services;
    }

    public static IServiceCollection AddApiWithSwagger(this IServiceCollection services, ConfigurationManager config)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(Constants.AllowAllCorsPolicy, builder => builder
                .WithOrigins(config["AllowedOrigin"]!)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
            );
        });
        services.AddEndpointsApiExplorer();
        services.AddControllers(options => options.Filters.Add<ValidateModelStateAttribute>());
        services.AddSignalR();
        services.AddSwaggerGen(c => {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "JWTToken_Auth_API",
                Version = "v1"
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IAuthRepository, AuthRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IGroupsRepository, GroupsRepository>();
        services.AddTransient<IRoomsRepository, RoomsRepository>();
        services.AddTransient<IMessagesRepository, MessagesRepository>();
        services.AddTransient<IWhiteboardRepository, WhiteboardRepository>();

        services.AddTransient<IEntityRepository<Group>, GroupsRepository>();

        services.AddTransient<IRequestHandler<GetGroupById.Query, GroupDto>, GetGroupById.Handler>(); 
        services.AddTransient<IRequestHandler<GetWhiteboardById.Query, WhiteboardDto>, GetWhiteboardById.Handler>();

        services.AddTransient<INotificationsService, NotificationService>();

        return services;
    }
}