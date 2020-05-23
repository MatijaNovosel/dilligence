using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class DiscussionAttachment
    {
        public int Id { get; set; }
        public int? FileId { get; set; }
        public int? DiscussionId { get; set; }
    }
}
