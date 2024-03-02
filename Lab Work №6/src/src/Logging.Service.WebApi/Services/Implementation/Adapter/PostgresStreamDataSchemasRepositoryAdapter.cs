using Logging.Server.Models.StreamData.Api.Schemas;
using Logging.Service.WebApi.Services.Interfaces;

namespace Logging.Service.WebApi.Services.Implementation.Adapter
{
    public class PostgresStreamDataSchemasRepositoryAdapter : IStreamDataSchemasRepository
    {
        private readonly PostgresDatabaseRepository _postgreSQLDatabase;

        public PostgresStreamDataSchemasRepositoryAdapter(PostgresDatabaseRepository postgreSQLDatabase)
        {
            _postgreSQLDatabase = postgreSQLDatabase;
        }

        public async Task Create(StreamDataSchemaPostViewModel value)
        {
            Console.WriteLine("Creating table in PostgreSQL...");
            await _postgreSQLDatabase.CreateTable(value);
        }

        public async Task<StreamDataSchemaViewModel?> Get(long streamId)
        {
            Console.WriteLine("Getting schema from PostgreSQL...");
            var schema = await _postgreSQLDatabase.GetSchema(streamId);
            return schema != null ? new StreamDataSchemaViewModel() : null;
        }

        public async Task<bool> Exists(long streamId)
        {
            Console.WriteLine("Checking if table exists in PostgreSQL...");
            return await _postgreSQLDatabase.TableExists(streamId);
        }

        public async Task Update(StreamDataSchemaPutViewModel value)
        {
            Console.WriteLine("Updating schema in PostgreSQL...");
            await _postgreSQLDatabase.UpdateSchema(value);
        }
    }
}
