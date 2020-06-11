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

  public class ExamUnfinishedDetailsQueryModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int TimeNeeded { get; set; }
    public string CreatedBy { get; set; }
    public int CreatedById { get; set; }
    public int CourseId { get; set; }
    public DateTime? DueDate { get; set; }
    public List<CreateQuestionDTO> Questions { get; set; }
  }

  public class ExamPreviewQueryModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int TimeNeeded { get; set; }
    public string CreatedBy { get; set; }
    public int CreatedById { get; set; }
    public int CourseId { get; set; }
    public DateTime? DueDate { get; set; }
    public List<QuestionDTO> Questions { get; set; }
  }
}
