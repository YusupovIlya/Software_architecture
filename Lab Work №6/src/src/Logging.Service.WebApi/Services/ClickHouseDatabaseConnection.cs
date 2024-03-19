using ClickHouse.Client.ADO;

namespace Logging.Service.WebApi.Services
{
    public class ClickHouseDatabaseConnection
    {
        private static readonly Lazy<ClickHouseConnection> _instance = new Lazy<ClickHouseConnection>(() =>
        {
            var connectionString = "Host=clickhouse;Port=8123;Username=someuser;Password=strongPasw;Database=logging_service;";
            return new ClickHouseConnection(connectionString);
        });

        public static ClickHouseConnection Instance => _instance.Value;

        private ClickHouseDatabaseConnection() { }

        public void ExecuteQuery(string query)
        {
            using (var cmd = Instance.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
