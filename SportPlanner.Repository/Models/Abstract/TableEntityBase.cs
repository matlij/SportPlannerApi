using Azure.Data.Tables;
using Azure;

namespace SportPlanner.Repository.Models.Abstract
{
    public class TableEntityBase : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }

}
