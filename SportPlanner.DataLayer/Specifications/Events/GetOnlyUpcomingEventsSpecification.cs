namespace SportPlanner.DataLayer.Specifications.Events
{
    public class GetOnlyUpcomingEventsSpecification : GetEventsSpecification
    {
        public GetOnlyUpcomingEventsSpecification() : base(e => e.Date >= DateTime.Now)
        {

        }
    }
}
