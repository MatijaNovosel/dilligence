using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class File
    {
        public File()
        {
            CourseTaskAttachment = new HashSet<CourseTaskAttachment>();
            DiscussionAttachment = new HashSet<DiscussionAttachment>();
            DiscussionCommentAttachment = new HashSet<DiscussionCommentAttachment>();
            DiscussionCommentImage = new HashSet<DiscussionCommentImage>();
            DiscussionImage = new HashSet<DiscussionImage>();
            NotificationFile = new HashSet<NotificationFile>();
            SidebarContentFile = new HashSet<SidebarContentFile>();
            TaskAttemptAttachment = new HashSet<TaskAttemptAttachment>();
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public long? Size { get; set; }

        public virtual ICollection<CourseTaskAttachment> CourseTaskAttachment { get; set; }
        public virtual ICollection<DiscussionAttachment> DiscussionAttachment { get; set; }
        public virtual ICollection<DiscussionCommentAttachment> DiscussionCommentAttachment { get; set; }
        public virtual ICollection<DiscussionCommentImage> DiscussionCommentImage { get; set; }
        public virtual ICollection<DiscussionImage> DiscussionImage { get; set; }
        public virtual ICollection<NotificationFile> NotificationFile { get; set; }
        public virtual ICollection<SidebarContentFile> SidebarContentFile { get; set; }
        public virtual ICollection<TaskAttemptAttachment> TaskAttemptAttachment { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
