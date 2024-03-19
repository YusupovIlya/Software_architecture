using Logging.Service.WebApi.Services.Interfaces;
using ILogger = Logging.Service.WebApi.Services.Interfaces.ILogger;

namespace Logging.Service.WebApi.Services.Implementation.Decorator
{
    public class LogLevelFilterDecorator : LoggerDecorator
    {
        public LogLevelFilterDecorator(ILogger logger, LogLevel minLogLevel)
            :base(logger, minLogLevel) { }

        public override void Log(string message, LogLevel logLevel)
        {
            if (logLevel >= _minLogLevel)
            {
                string timestampedMessage = $"[{DateTime.Now}] {message}";
                _logger.Log(timestampedMessage, logLevel);
            }
        }
    }
}
