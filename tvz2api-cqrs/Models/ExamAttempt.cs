using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class ExamAttempt
    {
        public ExamAttempt()
        {
            UserAnswer = new HashSet<UserAnswer>();
        }

        public int Id { get; set; }
        public bool? Terminated { get; set; }
        public bool? Started { get; set; }
        public DateTime? StartedAt { get; set; }
        public int? UserId { get; set; }
        public int? ExamId { get; set; }

        public virtual Exam Exam { get; set; }
        public virtual Korisnik User { get; set; }
        public virtual ICollection<UserAnswer> UserAnswer { get; set; }
    }
}
