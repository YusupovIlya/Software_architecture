using Logging.Service.WebApi.Services.Interfaces;

namespace Logging.Service.WebApi.Services
{
    public class LogPublisher
    {
        private List<ILogObserver> _observers = new List<ILogObserver>();

        // Метод для подписки наблюдателя
        public void Subscribe(ILogObserver observer)
        {
            _observers.Add(observer);
        }

        // Метод для отписки наблюдателя
        public void Unsubscribe(ILogObserver observer)
        {
            _observers.Remove(observer);
        }

        // Метод для публикации лога
        public void PublishLog(string log)
        {
            Console.WriteLine($"New log: {log}");

            // Уведомляем всех подписчиков о новом логе
            foreach (var observer in _observers)
            {
                observer.Update(log);
            }
        }
    }
}
