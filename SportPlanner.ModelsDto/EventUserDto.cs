using System;

namespace SportPlanner.ModelsDto
{
    public class EventUserDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int UserReply { get; set; }
        public bool IsOwner { get; set; }
    }
}
