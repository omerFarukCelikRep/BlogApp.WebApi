using BlogApp.Core.Utilities.Configurations;
using BlogApp.Core.Utilities.Constants;
using BlogApp.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.DataAccess.Extensions;
public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
    {
        services.AddDbContextPool<BlogAppDbContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString(DatabaseConstants.DefaultConnectionString), builder => builder.MigrationsAssembly(typeof(BlogAppDbContext).Assembly.FullName));

            options.UseLazyLoadingProxies();
        });

        return services;
    }
}
