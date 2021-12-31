using SportPlanner.DataLayer.Models;
using SportPlanner.DataLayer.Specifications.Abstract;

namespace SportPlanner.DataLayer.Specifications
{

    public class GetEventsByUserIdSpecification : SpecificationBase<Event>
    {
        public GetEventsByUserIdSpecification(Guid userId) : base(e => e.Users.Any(eu => eu.UserId == userId))
        {
        }
    }
}
