using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime? SentAt { get; set; }
        public int UserId { get; set; }
        public int ChatId { get; set; }

        public virtual Chat Chat { get; set; }
        public virtual User User { get; set; }
    }
}
