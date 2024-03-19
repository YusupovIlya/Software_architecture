using Logging.Service.WebApi.Services.Interfaces;

namespace Logging.Service.WebApi.Services.Implementation.LoggerStates
{
    public class LoggerContext
    {
        private ILoggerState _state;

        public LoggerContext()
        {
            // По умолчанию устанавливаем начальное состояние - Включено
            TransitionTo(new LoggingEnabledState());
        }

        // Метод для изменения состояния
        public void TransitionTo(ILoggerState state)
        {
            Console.WriteLine($"Switching state to {state.GetType().Name}");
            _state = state;
        }

        // Метод для логирования
        public void Log(string message)
        {
            _state.Log(message);
        }
    }
}
