using Logging.Service.WebApi.Services.Implementation.LogHandlers;
using Logging.Service.WebApi.Services.Interfaces;

namespace Logging.Service.WebApi.Services.Implementation.Loggers
{
    public class LoggerChain
    {
        private readonly ILogHandler _infoHandler;
        private readonly ILogHandler _warningHandler;

        public LoggerChain()
        {
            // Инициализация цепочки обработчиков
            _infoHandler = new InfoLogHandler();
            _warningHandler = new WarningLogHandler();

            // Установка следующего обработчика в цепочке
            _infoHandler.SetNextHandler(_warningHandler);
        }

        public void LogMessage(string message, LogLevel level)
        {
            // Начинаем обработку лога с первого обработчика в цепочке
            _infoHandler.HandleLog(message, level);
        }
    }
}
