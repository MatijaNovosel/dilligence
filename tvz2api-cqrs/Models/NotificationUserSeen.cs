using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class NotificationUserSeen
    {
        public int Id { get; set; }
        public int? NotificationId { get; set; }
        public int? UserId { get; set; }
        public bool? Seen { get; set; }

        public virtual Notification Notification { get; set; }
        public virtual User User { get; set; }
    }
}
