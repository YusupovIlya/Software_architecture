using Logging.Server.Service.StreamData.Models;
using Logging.Server.Service.StreamData.Services.Implementation;
using Logging.Service.WebApi.Services.Implementation.Loggers;

namespace Logging.Service.WebApi.Services.Implementation
{
    public class XlsxFileServiceProxy : FileService
    {
        private readonly XlsxFileService _fileService;
        public XlsxFileServiceProxy(string mime, XlsxFileService fileService):
            base(mime)
        {
            _fileService = fileService;
        }

        public override byte[] Prepare(Dictionary<long, IEnumerable<string>> streamFields, IEnumerable<string> orderedFields, IEnumerable<BaseStreamDataEvent> values)
        {
            LoggingFacade loggingFacade = new LoggingFacade();
            loggingFacade.Log($"exported streams = {string.Join(',', streamFields)}", LogLevel.Information);
            return _fileService.Prepare(streamFields, orderedFields, values);
        }
    }
}
