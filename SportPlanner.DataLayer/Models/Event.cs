namespace SportPlanner.DataLayer.Models;

public class Event : BaseEntity
{
    public DateTime Date { get; set; }
    public int EventType { get; set; }
    public IEnumerable<EventUser> Users { get; set; }

    public Address Address { get; set; }

    public Guid? AddressId { get; set; }
}
