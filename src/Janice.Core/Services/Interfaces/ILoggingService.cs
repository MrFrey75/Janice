namespace Janice.Core.Services.Interfaces
{
    public interface ILoggingService
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogDebug(string message);
        void LogEvent(string message);
        void AddFileSink(string filePath);
        void RemoveFileSink(string filePath);
        void EnableConsoleSink();
        void DisableConsoleSink();
        void EnableCliSink();
        void DisableCliSink();
    }
}
