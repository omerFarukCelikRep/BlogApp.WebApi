﻿using BlogApp.API.Constants;
using BlogApp.Core.Utilities.Constants;
using BlogApp.DataAccess.Contexts;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using System.Threading.RateLimiting;
using static BlogApp.API.Constants.ServiceCollectionConstants;

namespace BlogApp.API.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddCustomSwagger()
            .AddCustomVersioning()
            .AddCustomRateLimiter()
            .AddCustomOutputCaching()
            .AddHttpContextAccessor()
            .AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString(DatabaseConstants.DefaultConnectionString)!,
                          name: HealthCheckConstans.Name,
                          failureStatus: HealthStatus.Degraded,
                          timeout: TimeSpan.FromSeconds(HealthCheckConstans.TimeoutAsSeconds),
                          tags: new string[] { "services" }
                          )
            .AddDbContextCheck<BlogAppDbContext>();

        return services;
    }

    public static IServiceCollection AddCustomOutputCaching(this IServiceCollection services)
    {
        services.AddOutputCache(options =>
        {
            options.AddBasePolicy(builder =>
            {
                builder.Expire(TimeSpan.FromMinutes(5));
            });

            //options.AddPolicy("Basic", policy =>
            //{
            //    policy.Expire(TimeSpan.FromMinutes(10));
            //});
        });

        return services;
    }

    public static IServiceCollection AddCustomRateLimiter(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.AddPolicy(RateLimitConstans.PolicyName, context => RateLimitPartition.GetFixedWindowLimiter(
                partitionKey: context.User.Identity?.Name?.ToString(),
                factory: _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = RateLimitConstans.PermitLimit,
                    Window = TimeSpan.FromSeconds(RateLimitConstans.TimeWindow),
                    QueueLimit = RateLimitConstans.QueueLimit,
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                }));
        });

        return services;
    }
    public static IServiceCollection AddCustomVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });

        return services;
    }

    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "BlogApp",
                Version = "v1"
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = """
                Enter 'Bearer' [space] and then your valid token in the text input below.

                Example: "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9"
                """,
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                {
                    new OpenApiSecurityScheme{
                        Reference = new OpenApiReference{
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }
}