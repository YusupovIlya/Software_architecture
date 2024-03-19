using ILogger = Logging.Service.WebApi.Services.Interfaces.ILogger;

namespace Logging.Service.WebApi.Services.Implementation
{
    public class Logger : ILogger
    {
        public void Log(string message, LogLevel logLevel)
        {
            Console.WriteLine($"Log action: {message}");
        }
    }
}
