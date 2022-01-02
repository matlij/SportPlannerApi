using SportPlanner.DataLayer.Models;

namespace SportPlanner.DataLayer.Specifications.Events
{
    public class GetEventByIdSpecification : GetByIdSpecification<Event>
    {
        public GetEventByIdSpecification(Guid id) : base(id)
        {
            AddInclude(nameof(Event.Address));
            AddInclude($"{nameof(Event.Users)}.{nameof(EventUser.User)}");
        }
    }
}
