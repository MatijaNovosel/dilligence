using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class Subscription
    {
        public int Id { get; set; }
        public DateTime? JoinedAt { get; set; }
        public bool? Blacklisted { get; set; }
        public int? UserId { get; set; }
        public int? CourseId { get; set; }

        public virtual Course Course { get; set; }
        public virtual User User { get; set; }
    }
}
