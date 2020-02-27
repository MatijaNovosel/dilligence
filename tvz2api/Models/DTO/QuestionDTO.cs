using System;
using System.Collections.Generic;

namespace tvz2api.Models.DTO
{
    public class QuestionDTO : BaseDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int? ExamId { get; set; }
        public int? TypeId { get; set; }
        public IEnumerable<AnswerDTO> Answers { get; set; }
    }
}
