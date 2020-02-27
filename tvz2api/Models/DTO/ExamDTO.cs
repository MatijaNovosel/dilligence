using System;
using System.Collections.Generic;

namespace tvz2api.Models.DTO
{
    public class ExamDTO : BaseDTO
    {
        public string Naziv { get; set; }
        public decimal? Score { get; set; }
        public TimeSpan? TimeLeft { get; set; }
        public bool? Terminated { get; set; }
        public int? UserId { get; set; }
        public int? SubjectId { get; set; }
        public IEnumerable<QuestionDTO> Questions { get; set; }
    }
}
