using Logging.Service.WebApi.Services.Implementation.FileFactories;

namespace Logging.Server.Service.StreamData.Services.Implementation
{
    /// <summary>
    /// Фабрика по созданию сервисов формирования файлов.
    /// </summary>
    public static class FileServiceFactory
    {
        /// <summary>
        /// Создать сервис формирования файлов определённого типа.
        /// </summary>
        /// <param name="type">Тип файла.</param>
        public static FileService? Create(string? type) =>
            type switch
            {
                "csv" => new CsvFactory().CreateService("text/csv"),
                "xlsx" => new XlsxFactory().CreateService("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"),
                _ => null
            };
    }
}
