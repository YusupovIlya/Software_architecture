using Logging.Server.Models.StreamData.Api.Schemas;
using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Logging.Service.WebApi.Services.Implementation.Adapter;


public class PostgresDatabaseRepository
{
    private readonly string _connectionString;

    public PostgresDatabaseRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task CreateTable(StreamDataSchemaPostViewModel value)
    {
        // Создание подключения
        using var connection = CreateConnection();
        await connection.OpenAsync();

        // Создание команды SQL для создания таблицы
        string createTableSql = "CREATE TABLE IF NOT EXISTS YourTableNameHere (column1 datatype, column2 datatype, ...)";
        using var command = connection.CreateCommand();
        command.CommandText = createTableSql;

        // Выполнение команды
        await command.ExecuteNonQueryAsync();
    }

    public async Task<StreamDataSchemaViewModel?> GetSchema(long streamId)
    {
        // Создание подключения
        using var connection = CreateConnection();
        await connection.OpenAsync();

        // Создание команды SQL для выборки схемы
        string selectSchemaSql = "SELECT * FROM YourTableNameHere WHERE streamId = @streamId";
        using var command = connection.CreateCommand();
        command.CommandText = selectSchemaSql;

        // Выполнение команды и чтение результатов
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            // Преобразование данных из результата чтения в объект StreamDataSchemaViewModel
            return new StreamDataSchemaViewModel();
        }
        else
        {
            return null;
        }
    }

    public async Task<bool> TableExists(long streamId)
    {
        // Создание подключения
        using var connection = CreateConnection();
        await connection.OpenAsync();

        // Проверка существования таблицы с помощью механизма информационной схемы
        string checkTableExistsSql = "SELECT EXISTS (SELECT FROM information_schema.tables WHERE table_name = 'YourTableNameHere')";
        using var command = connection.CreateCommand();
        command.CommandText = checkTableExistsSql;

        // Получение результата запроса
        return true;
    }

    public async Task UpdateSchema(StreamDataSchemaPutViewModel value)
    {
        // Создание подключения
        using var connection = CreateConnection();
        await connection.OpenAsync();

        // Создание команды SQL для обновления схемы
        string updateSchemaSql = "UPDATE YourTableNameHere SET column1 = @value1, column2 = @value2 WHERE streamId = @streamId";
        using var command = connection.CreateCommand();
        command.CommandText = updateSchemaSql;

        // Выполнение команды
        await command.ExecuteNonQueryAsync();
    }

    private DbConnection CreateConnection()
    {
        // Создание объекта подключения к базе данных PostgreSQL
        var factory = DbProviderFactories.GetFactory("Npgsql");
        var connection = factory.CreateConnection();
        connection.ConnectionString = _connectionString;
        return connection;
    }
}
