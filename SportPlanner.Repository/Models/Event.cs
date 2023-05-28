using SportPlanner.Repository.Models.Abstract;

namespace SportPlanner.Repository.Models
{
    public class Event : TableEntity
    {
        public DateTimeOffset Date { get; set; }
        public int EventType { get; set; }
        public string? Address { get; set; }
    }
}
