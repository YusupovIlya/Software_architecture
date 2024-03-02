namespace Logging.Service.WebApi.Services.Implementation.Loggers
{
    public class FileLogger
    {
        public void LogToFile(string source, string message)
        {
            string timestampedMessage = $"[{DateTime.Now}] {message}";
            var log = new UpperCaseFormater().GetFormatedLog(timestampedMessage);
            File.WriteAllText(source, log);
        }
    }
}
