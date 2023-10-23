﻿using ClassLibrary.Models.Base;

namespace ClassLibrary.Models
{
    public class FriendLog : BaseEntity
    {
        public String Message { get; set; } = "";

        public Guid SenderId { get; set; }
        public User Sender { get; set; }
        
        public Guid FriendshipId { get; set; }
        public Friendship Friendship { get; set; }
    }
}
