namespace Logging.Service.WebApi.Services.Interfaces
{
    public abstract class FormatedLogTemplate
    {
        // Шаблонный метод для записи лога
        public string GetFormatedLog(string message)
        {
            string formattedMessage = FormatMessage(message);
            return formattedMessage + " " + GetLogTime();
        }

        // Абстрактный метод для форматирования сообщения
        protected abstract string FormatMessage(string message);

        protected virtual DateTime GetLogTime() => DateTime.Now;
    }
}
