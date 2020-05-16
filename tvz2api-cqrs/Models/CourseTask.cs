using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class CourseTask
    {
        public CourseTask()
        {
            CourseTaskAttachment = new HashSet<CourseTaskAttachment>();
            CourseTaskAttempt = new HashSet<CourseTaskAttempt>();
        }

        public int Id { get; set; }
        public int GradeMaximum { get; set; }
        public int CreatedById { get; set; }
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int CourseId { get; set; }
        public DateTime? SubmittedAt { get; set; }

        public virtual Course Course { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual ICollection<CourseTaskAttachment> CourseTaskAttachment { get; set; }
        public virtual ICollection<CourseTaskAttempt> CourseTaskAttempt { get; set; }
    }
}
