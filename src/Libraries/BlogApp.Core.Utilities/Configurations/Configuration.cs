using Microsoft.Extensions.Configuration;

namespace BlogApp.Core.Utilities.Configurations;
public class Configuration
{
    private static ConfigurationManager ConfigurationManager
    {
        get
        {
            ConfigurationManager configurationManager = new();

            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Libraries/BlogApp.API"));
            configurationManager.AddJsonFile("appsettings.Development.json");

            return configurationManager;
        }
    }

    public static string GetConnectionString(string connection)
    {
        return ConfigurationManager.GetConnectionString(connection);
    }

    public static string GetValue(string section)
    {
        return ConfigurationManager.GetSection(section).Value;
    }
}
