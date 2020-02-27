using System;
using System.Collections.Generic;

namespace tvz2api.Models.DTO
{
    public class AnswerDTO : BaseDTO
    {
        public string Content { get; set; }
        public bool? Correct { get; set; }
        public int? QuestionId { get; set; }
    }
}
