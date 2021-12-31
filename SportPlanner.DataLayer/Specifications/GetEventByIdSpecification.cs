using SportPlanner.DataLayer.Models;

namespace SportPlanner.DataLayer.Specifications
{
    public class GetEventByIdSpecification : GetEventsSpecification
    {
        public GetEventByIdSpecification(Guid id) : base(e => e.Id == id)
        {
            AddInclude(nameof(Event.Address));
            AddInclude($"{nameof(Event.Users)}.{nameof(EventUser.User)}");
        }
    }
}
