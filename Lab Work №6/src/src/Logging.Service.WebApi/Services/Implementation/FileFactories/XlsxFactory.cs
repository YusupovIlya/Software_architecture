using Logging.Server.Service.StreamData.Services.Implementation;

namespace Logging.Service.WebApi.Services.Implementation.FileFactories;

public class XlsxFactory : FileFactory
{
    public override FileService CreateService(string mimeType)
    {
        return new XlsxFileService("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }
}
