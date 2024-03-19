namespace Logging.Service.WebApi.Services.Implementation.Loggers
{
    public class ConsoleLogger
    {
        public void WriteLogToConsole(string message)
        {
            Console.WriteLine(message);
        }
    }
}
