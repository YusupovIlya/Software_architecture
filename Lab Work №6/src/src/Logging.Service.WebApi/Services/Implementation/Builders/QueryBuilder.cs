

namespace Logging.Service.WebApi.Services.Implementation.Builders
{
    public abstract class QueryBuilder
    {
        public abstract QueryBuilder AddSelect(string tableName);

        public abstract QueryBuilder AddWhere(string condition);

        public abstract string BuildQuery();
    }
}
