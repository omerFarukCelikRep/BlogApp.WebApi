using Elastic.Serilog.Sinks;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace BlogApp.Core.Utilities.LoggerServices.Serilog.Extensions;
public static class DependencyInjection
{
    public static IHostBuilder UseCustomSerilog(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog(
            (context, loggerConfiguration) =>
            {
                loggerConfiguration.Enrich.FromLogContext()
                                    .WriteTo.Console()
                                    .WriteTo.Elasticsearch([new Uri(context.Configuration["ElasticConfiguration:Uri"]!)])
                                    .ReadFrom.Configuration(context.Configuration);
            });

        return hostBuilder;
    }
}
