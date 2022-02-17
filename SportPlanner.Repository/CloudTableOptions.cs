using Azure.Data.Tables;

namespace SportPlanner.Repository;

public class CloudTableOptions<T> where T : ITableEntity
{
    public string ConnectionString { get; set; } = string.Empty;
    public string TableName { get; set; } = string.Empty;
}