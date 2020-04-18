using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models.DTO
{
  public class ExamDetailsDTO
  {
    public int Id { get; set; }
    public string Naziv { get; set; }
    public int TimeNeeded { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? DueDate { get; set; }
    public List<QuestionDTO> Questions { get; set; }
  }

  public class UpdateAttemptDTO
  {
    public int Id { get; set; }
    public List<QuestionDTO> Questions { get; set; }
  }
}
