using Logging.Service.WebApi.Services.Interfaces;
using System.Text.RegularExpressions;

namespace Logging.Service.WebApi.Services.Implementation.Strategies
{
    public class RemovePunctuationStrategy : IStringProcessingStrategy
    {
        public string ProcessString(string input)
        {
            return Regex.Replace(input, @"[^\w\s]", "");
        }
    }
}
