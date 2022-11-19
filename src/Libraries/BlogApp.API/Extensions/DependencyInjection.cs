using BlogApp.DataAccess.Contexts;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using System.Threading.RateLimiting;

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

        //TODO: Özel ayarlara bakılacak
        services.AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString("Default"),
                          name: "Database",
                          failureStatus: HealthStatus.Degraded,
                          timeout: TimeSpan.FromSeconds(1),
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
            options.AddFixedWindowLimiter("Basic", fixedWindowsOptions =>
            {
                fixedWindowsOptions.Window = TimeSpan.FromSeconds(12);
                fixedWindowsOptions.PermitLimit = 5;
                fixedWindowsOptions.QueueLimit = 2;
                fixedWindowsOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            });
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
                Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
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
