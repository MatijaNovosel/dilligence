using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class CourseTaskAttachment
    {
        public int Id { get; set; }
        public int FileId { get; set; }
        public int CourseTaskId { get; set; }

        public virtual CourseTask CourseTask { get; set; }
        public virtual File File { get; set; }
    }
}
