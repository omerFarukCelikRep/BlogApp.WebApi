using BlogApp.MVCUI.Filters;
using BlogApp.MVCUI.Handlers.Authentication;
using BlogApp.MVCUI.Services.Concretes;
using BlogApp.MVCUI.Services.Interfaces;

namespace BlogApp.MVCUI.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddMVCServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSession();

        services.AddAuthentication("Basic")
        .AddScheme<BlogAppAuthenticationSchemeOptions, BlogAppAuthenticationHandler>("Basic", null);

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
        services.AddScoped<ITopicService, TopicService>();
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICommentService, CommentService>();

        //services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareResultHandler>();

        services.AddControllersWithViews(options => options.Filters.Add<CustomExceptionFilter>());

        return services;
    }
}
