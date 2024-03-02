namespace Logging.Service.WebApi.Services.Implementation.Loggers
{
    public class LoggingFacade
    {
        private readonly ConsoleLogger _loggingSystem;
        private readonly FileLogger _fileLogger;
        private readonly PostgresLogger _databaseLogger;

        public LoggingFacade()
        {
            _loggingSystem = new ConsoleLogger();
            _fileLogger = new FileLogger();
            _databaseLogger = new PostgresLogger();
        }

        public void Log(string message, LogLevel logLevel)
        {
            _loggingSystem.WriteLogToConsole(message);

            // Сохранение логов в файл
            _fileLogger.LogToFile("temp_logs", $"[{DateTime.Now}] [{logLevel}] {message}");

            // Сохранение логов в базу данных
            _databaseLogger.WriteInternalLog($"[{DateTime.Now}] [{logLevel}] {message}", "internal_logs");
        }
    }
}
