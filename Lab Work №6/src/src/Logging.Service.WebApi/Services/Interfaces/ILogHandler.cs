namespace Logging.Service.WebApi.Services.Interfaces
{
    public interface ILogHandler
    {
        void HandleLog(string message, LogLevel level);
        void SetNextHandler(ILogHandler nextHandler);
    }
}
