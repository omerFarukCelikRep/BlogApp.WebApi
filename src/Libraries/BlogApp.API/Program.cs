using BlogApp.API.Extensions;
using BlogApp.Authentication;
using BlogApp.Authentication.Configurations;
using BlogApp.Business;
using BlogApp.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddDataAccessServices();

builder.Services.AddAuthenticationServices();

builder.Services.AddBusinessServices();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCustomVersioning();
//TODO: Özel ayarlara bakılacak
builder.Services.AddHealthChecks();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseHealthChecks("/health");

app.MapControllers();

app.Run();
