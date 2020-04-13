using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class Exam
    {
        public Exam()
        {
            ExamAttempt = new HashSet<ExamAttempt>();
            Question = new HashSet<Question>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public int TimeNeeded { get; set; }
        public int? SubjectId { get; set; }

        public virtual Kolegij Subject { get; set; }
        public virtual ICollection<ExamAttempt> ExamAttempt { get; set; }
        public virtual ICollection<Question> Question { get; set; }
    }
}
