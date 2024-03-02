using Logging.Service.WebApi.Services.Interfaces;

namespace Logging.Service.WebApi.Services.Implementation
{
    public class UpperCaseFormater : FormatedLogTemplate
    {
        protected override string FormatMessage(string message)
        {
            return $"{DateTime.Now.ToString("d")} || {message.ToUpper()}";
        }
    }
}
