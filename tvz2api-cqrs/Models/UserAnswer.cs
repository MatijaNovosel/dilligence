using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class UserAnswer
    {
        public int Id { get; set; }
        public int? AttemptId { get; set; }
        public int? QuestionId { get; set; }
        public int? AnswerId { get; set; }

        public virtual Answer Answer { get; set; }
        public virtual ExamAttempt Attempt { get; set; }
        public virtual Question Question { get; set; }
    }
}
