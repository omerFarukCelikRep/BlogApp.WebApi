using NLog;

namespace BlogApp.Core.Utilities.LoggerServices.NLoggerService;
public class LoggerService : ILoggerService
{
    private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

    public LoggerService() { }
    public void LogDebug(string message)
    {
        _logger.Debug(message);
    }

    public void LogError(string message)
    {
        _logger.Error(message);
    }

    public void LogInfo(string message)
    {
        _logger.Info(message);
    }

    public void LogWarning(string message)
    {
        _logger.Warn(message);
    }
}
