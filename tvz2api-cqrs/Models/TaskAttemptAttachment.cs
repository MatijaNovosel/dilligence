using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class TaskAttemptAttachment
    {
        public int Id { get; set; }
        public int FileId { get; set; }
        public int CourseTaskAttemptId { get; set; }

        public virtual CourseTaskAttempt CourseTaskAttempt { get; set; }
        public virtual File File { get; set; }
    }
}
