using BlogApp.Business.Concrete;
using BlogApp.Business.Interfaces;
using BlogApp.Business.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Business.Extensions;
public static class DependencyInjection
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddCustomValidation()
                .AddScoped<IAccountService, AccountService>()
                .AddScoped<ITopicService, TopicService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IArticleService, ArticleService>()
                .AddScoped<ICommentService, CommentService>();

        return services;
    }

    public static IServiceCollection AddCustomValidation(this IServiceCollection services)
    {
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<IValidator>();

        return services;
    }
}
