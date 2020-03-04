using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class Answer
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool? Correct { get; set; }
        public int? QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}
