﻿using BlogApp.DataAccess.EFCore.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.DataAccess.EFCore.Extensions;
public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessEFCoreServices(this IServiceCollection services)
    {
        services.AddTransient<ITopicRepository, TopicRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddTransient<IArticleRepository, ArticleRepository>();
        services.AddTransient<IPublishedArticleRepository, PublishedArticleRepository>();
        services.AddTransient<ICommentRepository, CommentRepository>();
        services.AddTransient<IUserSessionRepository, UserSessionRepository>();

        return services;
    }
}
