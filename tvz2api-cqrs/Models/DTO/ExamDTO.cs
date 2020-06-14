using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models.DTO
{
  public class ExamDTO
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Subject { get; set; }
    public int TimeNeeded { get; set; }
  }

  public class UnfinishedExamDTO
  {
    public int Id { get; set; }
    public int? CourseId { get; set; }
    public int? CreatedById { get; set; }
  }

  public class FinishedExamDTO
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Enabled { get; set; }
    public int? CourseId { get; set; }
    public int? CreatedById { get; set; }
  }

  public class ExamInProgressDTO
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Enabled { get; set; }
    public int? CourseId { get; set; }
    public int? CreatedById { get; set; }
    public int TimeNeeded { get; set; }
    public bool Expired { get; set; }
    public DateTime? StartedAt { get; set; }
  }

  public class CourseGradesTaskDTO
  {
    public string Name { get; set; }
    public int Grade { get; set; }
    public int MaximumGrade { get; set; }
  }

  public class CourseGradesExamDTO
  {
    public string Name { get; set; }
    public int Grade { get; set; }
    public int MaximumGrade { get; set; }
  }
}
