using Logging.Service.WebApi.Services.Interfaces;

namespace Logging.Service.WebApi.Services.Implementation.LogHandlers
{
    public class InfoLogHandler : BaseLogHandler
    {
        public override void HandleLog(string message, LogLevel level)
        {
            if (level == LogLevel.Information)
            {
                var log = new LowerCaseFormater().GetFormatedLog(message);
                Console.WriteLine($"Info Log: {message}");
            }
            else
            {
                // Передаем обработку следующему обработчику в цепочке
                base.HandleLog(message, level);
            }
        }
    }
}
