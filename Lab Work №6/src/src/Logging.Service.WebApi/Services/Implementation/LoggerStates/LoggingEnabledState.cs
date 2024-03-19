using Logging.Service.WebApi.Services.Interfaces;

namespace Logging.Service.WebApi.Services.Implementation.LoggerStates
{
    public class LoggingEnabledState : ILoggerState
    {
        public void Log(string message)
        {
            Console.WriteLine($"Logging enabled. Message logged: {message}");
        }
    }
}
