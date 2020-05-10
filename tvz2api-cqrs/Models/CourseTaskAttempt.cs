using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class CourseTaskAttempt
    {
        public CourseTaskAttempt()
        {
            TaskAttemptAttachment = new HashSet<TaskAttemptAttachment>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseTaskId { get; set; }

        public virtual CourseTask CourseTask { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<TaskAttemptAttachment> TaskAttemptAttachment { get; set; }
    }
}
