namespace Logging.Service.WebApi.Services.Implementation.Builders
{
    public class ClickHouseQueryClient
    {
        private QueryBuilder _queryBuilder;

        public ClickHouseQueryClient(QueryBuilder queryBuilder)
        {

            _queryBuilder = queryBuilder;
        }

        public string ConstructQuery() =>
            _queryBuilder
                .AddSelect("logs")
                .AddWhere("\"userId\" = 12")
                .BuildQuery();
    }
}
