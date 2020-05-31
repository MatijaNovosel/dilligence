using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class DiscussionComment
    {
        public DiscussionComment()
        {
            DiscussionCommentAttachment = new HashSet<DiscussionCommentAttachment>();
            DiscussionCommentImage = new HashSet<DiscussionCommentImage>();
        }

        public int Id { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public string Content { get; set; }
        public int? DiscussionId { get; set; }
        public int? SubmittedById { get; set; }

        public virtual Discussion Discussion { get; set; }
        public virtual User SubmittedBy { get; set; }
        public virtual ICollection<DiscussionCommentAttachment> DiscussionCommentAttachment { get; set; }
        public virtual ICollection<DiscussionCommentImage> DiscussionCommentImage { get; set; }
    }
}
