using Logging.Service.WebApi.Services.Interfaces;

namespace Logging.Service.WebApi.Services.Implementation.LogHandlers
{
    public class WarningLogHandler : BaseLogHandler
    {
        private string _uriWarningLogs = "uri";
        private const string DefaultAddress = "uri";
        public override void HandleLog(string message, LogLevel level)
        {
            if (level == LogLevel.Warning && _uriWarningLogs != DefaultAddress)
            {
                var client = new HttpClient();
                client.PostAsync(_uriWarningLogs, new StringContent(message));
            }
            else
            {
                // Передаем обработку следующему обработчику в цепочке
                base.HandleLog(message, level);
            }
        }
    }
}
