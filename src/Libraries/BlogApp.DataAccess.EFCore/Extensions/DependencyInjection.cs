﻿using BlogApp.DataAccess.EFCore.Repositories;
using BlogApp.DataAccess.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.DataAccess.EFCore.Extensions;
public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessEFCoreServices(this IServiceCollection services)
    {
        services.AddTransient<ITopicRepository, TopicRepository>();
        services.AddTransient<IMemberRepository, MemberRepository>();
        services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddTransient<IArticleRepository, ArticleRepository>();
        services.AddTransient<IPublishedArticleRepository, PublishedArticleRepository>();

        return services;
    }
}