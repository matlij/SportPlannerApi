using Azure.Data.Tables;

namespace SportPlanner.Repository.Interfaces;

public interface ICloudTableClient<T> where T : ITableEntity
{
    public TableClient Client { get; set; }
}
