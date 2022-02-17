using Azure;
using Azure.Data.Tables;

namespace SportPlanner.Repository;

public static class TableClientExtensions
{
    public static async Task<IEnumerable<T>> GetFromResponse<T>(this AsyncPageable<T> response) where T : ITableEntity
    {
        var entities = new List<T>();
        await foreach (var entity in response)
        {
            entities.Add(entity);
        }

        return entities;
    }
}