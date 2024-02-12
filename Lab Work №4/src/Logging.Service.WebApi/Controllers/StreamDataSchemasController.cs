using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Logging.Server.Models.StreamData.Api.Schemas;
using Logging.Server.Service.StreamData.Services;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Logging.Server.Service.StreamData.Controllers
{
    /// <summary>
    /// Контроллер схем потоковых данных.
    /// </summary>
    //[Authorize]
    [ApiController]
    [Route("api/cl/stream-data/schemas")]
    public class StreamDataSchemasController : ControllerBase
    {
        readonly IStreamDataSchemasRepository _streamDataSchemas;
        //readonly IStringLocalizer _localizer;

        /// <summary>
        /// Конструктор контроллера схем потоковых данных.
        /// Создаёт новый экземпляр класса <see cref="StreamDataSchemasController"/>
        /// </summary>
        public StreamDataSchemasController(IStreamDataSchemasRepository streamDataSchemas)
        {
            _streamDataSchemas = streamDataSchemas;
        }

        /// <summary>
        /// Получить схему потоковых данных по идентификатору.
        /// </summary>
        /// <param name="streamId">Идентификатор потока данных.</param>
        /// <returns>Модель схемы потоковых данных (см. <see cref="StreamDataSchemaViewModel"/>).</returns>
        [HttpGet("{streamId}")]
        [ValidateActionParameters]
        public async Task<ActionResult<StreamDataSchemaViewModel?>> Get(
            [Range(1, long.MaxValue, ErrorMessage = "Stream identifier is incorrect.")] long streamId)
        {
            var schema = await _streamDataSchemas.Get(streamId);
            return schema;
        }

        /// <summary>
        /// Создать новую схему потоковых данных.
        /// </summary>
        /// <param name="value">Модель для создания схемы потоковых данных (см. <see cref="StreamDataSchemaPostViewModel"/>).</param>
        [HttpPost]
        [ValidateActionParameters]
        public async Task<IActionResult> Create(
            [FromBody] StreamDataSchemaPostViewModel value)
        {
            await _streamDataSchemas.Create(value);
            return NoContent();
        }

        /// <summary>
        /// Обновить схему потоковых данных.
        /// </summary>
        /// <param name="value">Модель для обновления схемы потоковых данных (см. <see cref="StreamDataSchemaPutViewModel"/>).</param>
        [HttpPut]
        [ValidateActionParameters]
        public async Task<IActionResult> Put(
            [FromBody] StreamDataSchemaPutViewModel value)
        {
            var exists = await _streamDataSchemas.Exists(value.StreamId);
            if (!exists)
                return NotFound(new ErrorResponseViewModel("Не найдена схема!"));

            await _streamDataSchemas.Update(value);
            return NoContent();
        }

        [HttpDelete("{streamId}")]
        [ValidateActionParameters]
        public async Task<IActionResult> Delete([Range(1, long.MaxValue, ErrorMessage = "Stream identifier is incorrect.")] long streamId)
        {
            await _streamDataSchemas.Delete(value);
            return NoContent();
        }
    }
}
