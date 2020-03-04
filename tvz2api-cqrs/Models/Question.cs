using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class Question
    {
        public Question()
        {
            Answer = new HashSet<Answer>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? ExamId { get; set; }
        public int? TypeId { get; set; }

        public virtual Exam Exam { get; set; }
        public virtual QuestionType Type { get; set; }
        public virtual ICollection<Answer> Answer { get; set; }
    }
}
