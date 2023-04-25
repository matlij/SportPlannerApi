using SportPlanner.Repository.Models.Abstract;

namespace SportPlanner.Repository.Models
{
    public class Event : TableEntity
    {
        public DateTimeOffset Date { get; set; }
        public int EventType { get; set; }
        public Guid AddressId { get; set; } = Guid.Empty;
    }

    public class EventUser : TableEntity
    {
        public string Name { get; set; } = string.Empty;
        public int UserReply { get; set; }
        public bool IsOwner { get; set; }
    }
}
