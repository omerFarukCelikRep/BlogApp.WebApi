using BlogApp.MVCUI.Handlers;

namespace BlogApp.MVCUI.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddMVCServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient("WebApiClient", client =>
        {
            client.BaseAddress = new Uri(configuration["WebApiClient:Url"]);
        })
        .AddHttpMessageHandler<AuthTokenHandler>();

        services.AddScoped(serviceProvider =>
        {
            var clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

            return clientFactory.CreateClient("WebApiClient");
        });

        services.AddControllersWithViews();

        return services;
    }
}
