using BlogApp.MVCUI.Handlers;
using BlogApp.MVCUI.Services.Concretes;
using BlogApp.MVCUI.Services.Interfaces;

namespace BlogApp.MVCUI.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddMVCServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSession();
        services.AddScoped<AuthTokenHandler>();
        services.AddHttpClient("WebApiClient", client =>
        {
            client.BaseAddress = new Uri(configuration["WebApiClient:Url"]);
        })
        .AddHttpMessageHandler<AuthTokenHandler>();
        services.AddHttpContextAccessor();

        services.AddScoped(serviceProvider =>
        {
            var clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

            return clientFactory.CreateClient("WebApiClient");
        });
        services.AddScoped<IIdentityService, IdentityService>();

        services.AddControllersWithViews();



        return services;
    }
}
