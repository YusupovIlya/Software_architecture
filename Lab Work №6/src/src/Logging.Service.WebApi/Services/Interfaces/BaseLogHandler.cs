namespace Logging.Service.WebApi.Services.Interfaces
{
    public abstract class BaseLogHandler : ILogHandler
    {
        private ILogHandler _nextHandler;

        public void SetNextHandler(ILogHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }

        public virtual void HandleLog(string message, LogLevel level)
        {
            // Если есть следующий обработчик в цепочке, передаем ему обработку лога
            _nextHandler?.HandleLog(message, level);
        }
    }
}
