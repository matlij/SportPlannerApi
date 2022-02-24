using SportPlanner.ModelsDto.Enums;

namespace SportPlanner.ModelsDto
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public EventType EventType { get; set; }
        public AddressDto Address { get; set; } = new AddressDto();
        public IEnumerable<EventUserDto> Users { get; set; } = Enumerable.Empty<EventUserDto>();
    }
}
