using Logging.Server.Service.StreamData.Services.Implementation;

namespace Logging.Service.WebApi.Services.Implementation.FileFactories
{
    public abstract class FileFactory
    {
        public abstract FileService CreateService(string mimeType);
    }
}
