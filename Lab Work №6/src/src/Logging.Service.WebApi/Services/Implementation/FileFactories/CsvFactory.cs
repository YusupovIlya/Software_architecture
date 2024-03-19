using Logging.Server.Service.StreamData.Services.Implementation;

namespace Logging.Service.WebApi.Services.Implementation.FileFactories
{
    public class CsvFactory : FileFactory
    {
        public override FileService CreateService(string mimeType)
        {
            return new CsvFileService("text/csv");
        }
    }
}
