using BlogApp.Business.Concrete;
using BlogApp.Business.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Business.Extensions;
public static class DependencyInjection
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddCustomValidation();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITopicService, TopicService>();
        services.AddScoped<IMemberService, MemberService>();
        services.AddScoped<IArticleService, ArticleService>();

        return services;
    }

    public static IServiceCollection AddCustomValidation(this IServiceCollection services)
    {
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<IValidator>();

        return services;
    }
}
