using BlogApp.Business.Abstract;
using BlogApp.Business.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Business;
public static class BusinessServiceRegistration
{
    public static void AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITopicService, TopicService>();
    }
}
