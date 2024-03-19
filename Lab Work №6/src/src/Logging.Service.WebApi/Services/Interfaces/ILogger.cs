namespace Logging.Service.WebApi.Services.Interfaces
{
    public interface ILogger
    {
        void Log(string message, LogLevel logLevel);
    }
}
