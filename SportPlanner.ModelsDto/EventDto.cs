using SportPlanner.ModelsDto.Enums;
using System;
using System.Collections.Generic;

namespace SportPlanner.ModelsDto
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public EventType EventType { get; set; }
        public AddressDto Address { get; set; }
        public IEnumerable<EventUserDto> Users { get; set; }
    }
}
