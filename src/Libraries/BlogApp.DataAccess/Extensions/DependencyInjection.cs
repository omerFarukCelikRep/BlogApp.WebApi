using BlogApp.Core.DataAccess.Interceptors;
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
        services.AddDbContext<BlogAppDbContext>((sp, options) =>
        {
            options.UseSqlServer(Configuration.GetConnectionString(DatabaseConstants.DefaultConnectionString), builder => builder.MigrationsAssembly(typeof(BlogAppDbContext).Assembly.FullName))
                   .AddInterceptors(sp.GetRequiredService<AuditableInterceptor>());

            options.UseLazyLoadingProxies();
        });

        return services;
    }
}
