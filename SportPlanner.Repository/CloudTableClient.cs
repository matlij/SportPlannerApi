using Azure.Data.Tables;
using Microsoft.Extensions.Options;
using SportPlanner.Repository.Interfaces;

namespace SportPlanner.Repository;

public class CloudTableClient<T> : ICloudTableClient<T> where T : ITableEntity
{
    public TableClient Client { get; set; }
    public CloudTableClient(IOptions<CloudTableOptions<T>> options)
    {
        Client = new TableClient(options.Value.ConnectionString, options.Value.TableName);
        Client.CreateIfNotExists();
    }
}
