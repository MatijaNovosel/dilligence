using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class NotificationFile
    {
        public int Id { get; set; }
        public int NotificationId { get; set; }
        public int FileId { get; set; }

        public virtual File File { get; set; }
        public virtual Notification Notification { get; set; }
    }
}
