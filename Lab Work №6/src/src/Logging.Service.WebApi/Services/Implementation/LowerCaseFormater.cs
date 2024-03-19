using Logging.Service.WebApi.Services.Interfaces;

namespace Logging.Service.WebApi.Services.Implementation
{
    public class LowerCaseFormater : FormatedLogTemplate
    {
        protected override string FormatMessage(string message)
        {
            return $"{DateTime.Now.ToString("f")} || {message.ToLower()}";
        }

        protected override DateTime GetLogTime()
        {
            return DateTime.Now.AddHours(-1);
        }
    }
}
