using Npgsql;

namespace Logging.Service.WebApi.Services.Implementation.Loggers
{
    public class PostgresLogger
    {
        public void WriteInternalLog(string message, string dest)
        {
            using (var connection = new NpgsqlConnection("postgres"))
            {
                connection.Open();

                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;

                    // Подготовка SQL-запроса для вставки лога
                    command.CommandText = "INSERT INTO internal_logs (message, destination, created_at) VALUES (@message, @dest, @createdAt)";
                    command.Parameters.AddWithValue("@message", message);
                    command.Parameters.AddWithValue("@dest", dest);
                    command.Parameters.AddWithValue("@createdAt", DateTime.Now);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
