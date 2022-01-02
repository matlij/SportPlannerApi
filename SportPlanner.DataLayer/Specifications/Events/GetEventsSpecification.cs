using SportPlanner.DataLayer.Models;
using SportPlanner.DataLayer.Specifications.Abstract;
using System.Linq.Expressions;

namespace SportPlanner.DataLayer.Specifications.Events
{
    public class GetEventsSpecification : SpecificationBase<Event>
    {
        public GetEventsSpecification(Expression<Func<Event, bool>> filter) : base(filter)
        {
            AddInclude(nameof(Event.Address));
            AddInclude($"{nameof(Event.Users)}.{nameof(EventUser.User)}");
        }
    }
}
