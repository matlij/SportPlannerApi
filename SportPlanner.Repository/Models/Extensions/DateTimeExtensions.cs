namespace SportPlanner.Repository.Models.Extensions;
public static class DateTimeExtensions
{
    public static string ToEventPartitionKeyString(this DateTimeOffset date)
    {
        return date.ToString("yyyyMMdd");
    }

    public static int ToEventPartitionKeyInt(this DateTimeOffset date)
    {
        return int.Parse(date.ToEventPartitionKeyString());
    }
}
