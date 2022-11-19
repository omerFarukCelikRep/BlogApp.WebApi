using BlogApp.API.Extensions;
using BlogApp.API.Middlewares;
using BlogApp.API.Responses;
using BlogApp.Authentication.Extensions;
using BlogApp.Business.Extensions;
using BlogApp.Core.Utilities.LoggerServices.Serilog.Extensions;
using BlogApp.DataAccess.EFCore.Extensions;
using BlogApp.DataAccess.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services
            .AddDataAccessServices()
            .AddDataAccessEFCoreServices()
            .AddAuthenticationServices()
            .AddBusinessServices()
            .AddApiServices(builder.Configuration);

builder.Host.UseCustomSerilog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRateLimiter();

app.UseOutputCache();

app.MapHealthChecks("/healthz", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var response = new HealthCheckReponse
        {
            Status = report.Status.ToString(),
            HealthChecks = report.Entries.Select(x => new IndividualHealthCheckResponse
            {
                Component = x.Key,
                Status = x.Value.Status.ToString(),
                Description = x.Value.Description
            }),
            HealthCheckDuration = report.TotalDuration
        };
        await context.Response.WriteAsJsonAsync(response);
    }
});

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
