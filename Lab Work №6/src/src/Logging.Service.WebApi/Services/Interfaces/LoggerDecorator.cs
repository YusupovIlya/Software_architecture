namespace Logging.Service.WebApi.Services.Interfaces;

public abstract class LoggerDecorator : ILogger
{
    protected readonly ILogger _logger;
    protected readonly LogLevel _minLogLevel;
    public LoggerDecorator(ILogger logger, LogLevel minLogLevel)
    {
        _logger = logger;
        _minLogLevel = minLogLevel;
    }

    public abstract void Log(string message, LogLevel logLevel);
}
