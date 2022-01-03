namespace SportPlanner.DataLayer.Specifications.Events
{

    public class GetEventsByUserIdSpecification : GetEventsSpecification
    {
        public GetEventsByUserIdSpecification(Guid userId) : base(e => e.Users.Any(eu => eu.UserId == userId))
        {
        }
    }
}
