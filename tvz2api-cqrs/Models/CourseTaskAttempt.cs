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
        public string Description { get; set; }
        public int Grade { get; set; }
        public int UserId { get; set; }
        public int CourseTaskId { get; set; }
        public int GradedById { get; set; }

        public virtual CourseTask CourseTask { get; set; }
        public virtual User GradedBy { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<TaskAttemptAttachment> TaskAttemptAttachment { get; set; }
    }
}
