namespace BlogApp.Core.Utilities.LoggerServices;
public interface ILoggerService
{
    void LogDebug(string message);
    void LogError(string message);
    void LogInfo(string message);
    void LogWarning(string message);
}
