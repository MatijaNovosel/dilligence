using System;
using System.Collections.Generic;

namespace tvz2api.Models
{
    public partial class QuestionType
    {
        public QuestionType()
        {
            Question = new HashSet<Question>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Question> Question { get; set; }
    }
}
