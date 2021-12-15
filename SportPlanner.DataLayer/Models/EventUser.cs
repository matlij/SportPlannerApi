﻿using System;
using System.ComponentModel.DataAnnotations;

namespace SportPlanner.DataLayer.Models
{
    public class EventUser
    {
        public Guid EventId { get; set; }
        public Event Event { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        [Required]
        public bool IsOwner { get; set; }

        public int UserReply { get; set; }
    }
}
