using Logging.Service.WebApi.Services.Interfaces;

namespace Logging.Service.WebApi.Services.Implementation.LoggerStates
{
    public class LoggingDisabledState : ILoggerState
    {
        public void Log(string message)
        {
            Console.WriteLine($"Logging is disabled. Cannot log message: {message}");
        }
    }
}
