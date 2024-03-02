using Logging.Service.WebApi.Services.Interfaces;
using ILogger = Logging.Service.WebApi.Services.Interfaces.ILogger;

namespace Logging.Service.WebApi.Services.Implementation.Decorator
{
    public class ConditionalLoggerDecorator : LoggerDecorator
    {
        private const int minLenght = 200;
        public ConditionalLoggerDecorator(ILogger logger, LogLevel minLogLevel)
            : base(logger, minLogLevel) { }

        public override void Log(string message, LogLevel logLevel)
        {
            if (message.Length >= minLenght)
            {
                string timestampedMessage = $"[{DateTime.Now}] {message}";
                File.AppendAllText(timestampedMessage, message);
            }
            else
            {
                _logger.Log(message, logLevel);
            }
        }
    }
}
