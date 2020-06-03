using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class Discussion
    {
        public Discussion()
        {
            DiscussionComment = new HashSet<DiscussionComment>();
        }

        public int Id { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public string Content { get; set; }
        public int? CourseId { get; set; }
        public int? SubmittedById { get; set; }

        public virtual Course Course { get; set; }
        public virtual User SubmittedBy { get; set; }
        public virtual ICollection<DiscussionComment> DiscussionComment { get; set; }
    }
}
