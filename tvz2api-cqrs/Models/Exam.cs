using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class Exam
    {
        public Exam()
        {
            Question = new HashSet<Question>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public decimal? Score { get; set; }
        public TimeSpan? TimeLeft { get; set; }
        public bool? Terminated { get; set; }
        public int? UserId { get; set; }
        public int? SubjectId { get; set; }

        public virtual Kolegij Subject { get; set; }
        public virtual Korisnik User { get; set; }
        public virtual ICollection<Question> Question { get; set; }
    }
}
