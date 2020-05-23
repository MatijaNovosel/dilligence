using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class Discussion
    {
        public int Id { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public string Content { get; set; }
        public string BackgroundColor { get; set; }
        public string TextColor { get; set; }
        public int? CourseId { get; set; }
        public int? SubmittedById { get; set; }
    }
}
