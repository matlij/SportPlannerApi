namespace SportPlanner.Repository.Models.Extensions;
public static class DateTimeExtensions
{
    public static string ToEventPartitionKey(this DateTimeOffset date)
    {
        return date.ToString("yyyyMMdd");
    }
}
