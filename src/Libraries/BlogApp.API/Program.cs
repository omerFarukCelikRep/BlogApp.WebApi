using BlogApp.API.Extensions;
using BlogApp.API.Middlewares;
using BlogApp.Authentication;
using BlogApp.Business;
using BlogApp.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccessServices();

builder.Services.AddAuthenticationServices();

builder.Services.AddBusinessServices();

builder.Services.AddControllers()
    .AddCustomValidation();

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
