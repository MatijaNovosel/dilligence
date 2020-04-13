using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models.DTO
{
  public class ExamDetailsDTO
  {
    public int Id { get; set; }
    public string Naziv { get; set; }
    public int TimeNeeded { get; set; }
    public List<QuestionDTO> Questions { get; set; }
  }
  public class UpdateExamDetailsDTO
  {
    public int Id { get; set; }
    public List<QuestionDTO> Questions { get; set; }
  }
}
