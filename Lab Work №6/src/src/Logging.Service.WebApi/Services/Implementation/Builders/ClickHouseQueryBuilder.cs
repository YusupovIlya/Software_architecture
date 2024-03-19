using System.Text;

namespace Logging.Service.WebApi.Services.Implementation.Builders;

public class ClickHouseQueryBuilder : QueryBuilder
{
    private String _query = string.Empty;

    public override QueryBuilder AddSelect(string tableName)
    {
        _query += $"SELECT FROM {tableName} ";
        return this;
    }

    public override QueryBuilder AddWhere(string condition)
    {
        _query += " WHERE " + condition;
        return this;
    }

    public override string BuildQuery()
    {
        return _query;
    }
}
