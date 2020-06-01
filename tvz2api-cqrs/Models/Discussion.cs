using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class Discussion
    {
        public Discussion()
        {
            DiscussionAttachment = new HashSet<DiscussionAttachment>();
            DiscussionComment = new HashSet<DiscussionComment>();
            DiscussionImage = new HashSet<DiscussionImage>();
        }

        public int Id { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public string Content { get; set; }
        public string BackgroundColor { get; set; }
        public string TextColor { get; set; }
        public int? CourseId { get; set; }
        public int? SubmittedById { get; set; }

        public virtual Course Course { get; set; }
        public virtual User SubmittedBy { get; set; }
        public virtual ICollection<DiscussionAttachment> DiscussionAttachment { get; set; }
        public virtual ICollection<DiscussionComment> DiscussionComment { get; set; }
        public virtual ICollection<DiscussionImage> DiscussionImage { get; set; }
    }
}
