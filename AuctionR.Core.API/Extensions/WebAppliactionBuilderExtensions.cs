using AuctionR.Core.API.ExceptionHandling;
using AuctionR.Core.API.Services;
using AuctionR.Core.Application.Common.Exceptions;
using AuctionR.Core.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using System.Threading.RateLimiting;

namespace AuctionR.Core.API.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSignalR();

        builder.AddSwagger();
        builder.AddSerilogLogging();
        builder.AddRateLimiting();
        builder.AddJwtAuthentication();
        builder.AddAuthorizationWithPolicies();

        builder.Services.AddScoped<INotifier, SignalRNotifier>();

        return builder;
    }

    private static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "bearerAuth"
                        }
                    },
                    []
                }
            });
        });

        return builder;
    }

    private static WebApplicationBuilder AddSerilogLogging(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));

        return builder;
    }

    private static WebApplicationBuilder AddRateLimiting(this WebApplicationBuilder builder)
    {
        builder.Services.AddRateLimiter(options =>
        {
            var config = builder.Configuration.GetSection("RateLimiting");
            int permitLimit = config.GetValue<int>("PermitLimit");
            int windowSeconds = config.GetValue<int>("WindowSeconds");

            options.AddPolicy("Fixed", httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = permitLimit,
                        Window = TimeSpan.FromSeconds(windowSeconds),
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 0
                    }));

            options.OnRejected = async (context, _) =>
            {
                await ExceptionHandler.HandleRateLimitExceptionAsync(context.HttpContext);
            };
        });

        return builder;
    }

    private static WebApplicationBuilder AddJwtAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            var jwt = builder.Configuration.GetSection("Jwt");
            string? issuer = jwt.GetValue<string>("Issuer");
            string? audience = jwt.GetValue<string>("Audience");
            string? key = jwt.GetValue<string>("Key");
            int clockSkewSeconds = jwt.GetValue<int>("ClockSkewSeconds", 0);

            ArgumentNullException.ThrowIfNullOrWhiteSpace(issuer, nameof(issuer));
            ArgumentNullException.ThrowIfNullOrWhiteSpace(audience, nameof(audience));
            ArgumentNullException.ThrowIfNullOrWhiteSpace(key, nameof(key));

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(clockSkewSeconds),
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };

            options.Events = new JwtBearerEvents
            {
                OnChallenge = async context =>
                {
                    context.HandleResponse();

                    await ExceptionHandler.HandleUnauthorizedExceptionAsync(context.HttpContext);
                },

                OnForbidden = async context =>
                {
                    await ExceptionHandler.HandleForbiddenExceptionAsync(context.HttpContext, new ForbiddenException());
                }
            };
        });

        return builder;
    }

    private static WebApplicationBuilder AddAuthorizationWithPolicies(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization(options =>
        {
            var permissionsSection = builder.Configuration
                                        .GetSection("Permissions")
                                        .GetChildren();

            foreach (var resourceSection in permissionsSection)
            {
                var resourceName = resourceSection.Key;
                var resourceChildren = resourceSection.GetChildren();

                foreach (var action in resourceChildren)
                {
                    var actionName = action.Key;
                    var permissionValue = action.Value;

                    if (!string.IsNullOrWhiteSpace(permissionValue))
                    {
                        var policyName = $"{resourceName}.{actionName}".ToLowerInvariant();

                        options.AddPolicy(policyName, policy =>
                        {
                            policy.RequireClaim("permissions", permissionValue);
                        });
                    }
                }
            }
        });

        return builder;
    }
}