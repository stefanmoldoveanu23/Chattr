﻿using ClassLibrary.Models.Enums;
using ClassLibrary.Models.Base;

namespace ClassLibrary.Models
{
    public class Settings : BaseEntity
    {
        public Themes Theme { get; set; }
        public bool Notifs { get; set; }
        public bool Appearance { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
