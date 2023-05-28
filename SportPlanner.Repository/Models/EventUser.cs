using SportPlanner.Repository.Models.Abstract;

namespace SportPlanner.Repository.Models
{
    public class EventUser : TableEntity
    {
        public string Name { get; set; } = string.Empty;
        public int UserReply { get; set; }
        public bool IsOwner { get; set; }
    }
}
