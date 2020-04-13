using tvz2api_cqrs.Models.DTO;
using System.Collections.Generic;
using System;

namespace tvz2api_cqrs.QueryModels
{
  public class ExamAttemptDetailsQueryModel
  {
    public int Id { get; set; }
    public bool? Terminated { get; set; }
    public bool? Started { get; set; }
    public DateTime? StartedAt { get; set; }
    public ExamDetailsDTO Exam { get; set; }
  }

  public class ExamAttemptQueryModel
  {
    public int Id { get; set; }
    public bool? Terminated { get; set; }
    public DateTime? StartedAt { get; set; }
    public bool? Started { get; set; }
    public ExamDTO Exam { get; set; }
  }
}
