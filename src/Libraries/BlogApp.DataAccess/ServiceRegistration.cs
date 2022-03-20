using BlogApp.Core.Utilities.Configurations;
using BlogApp.DataAccess.Abstract;
using BlogApp.DataAccess.Contexts;
using BlogApp.DataAccess.EFCore.Repositories;
using BlogApp.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.DataAccess;
public static class ServiceRegistration
{
    public static void AddDataAccessServices(this IServiceCollection services)
    {
        services.AddDbContext<BlogAppDbContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), builder => builder.MigrationsAssembly(typeof(BlogAppDbContext).Assembly.FullName));
        });

        services.AddScoped<ITopicRepository, TopicRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
    }
}
