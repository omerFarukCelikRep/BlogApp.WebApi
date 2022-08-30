using BlogApp.API.Extensions;
using BlogApp.API.Middlewares;
using BlogApp.Authentication.Extensions;
using BlogApp.Business.Extensions;
using BlogApp.DataAccess.EFCore.Extensions;
using BlogApp.DataAccess.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
            .AddDataAccessServices()
            .AddDataAccessEFCoreServices()
            .AddAuthenticationServices()
            .AddBusinessServices();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCustomSwagger();

builder.Services.AddCustomVersioning();
//TODO: Özel ayarlara bakılacak
builder.Services.AddHealthChecks();

builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseHealthChecks("/health");

app.MapControllers();

app.Run();
