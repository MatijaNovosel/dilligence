using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tvz2api_cqrs.Infrastructure.Commands;
using tvz2api_cqrs.Models.DTO;

namespace tvz2api_cqrs.Implementation.Commands
{
  public class CourseTaskCreateCommand : ICommand
  {
    public CourseTaskCreateCommand() { }
    [FromForm(Name = "createdById")]
    public int CreatedById { get; set; }
    [FromForm(Name = "courseId")]
    public int CourseId { get; set; }
    [FromForm(Name = "title")]
    public string Title { get; set; }
    [FromForm(Name = "description")]
    public string Description { get; set; }
    [FromForm(Name = "dueDate")]
    public DateTime DueDate { get; set; }
    [FromForm(Name = "sendEmail")]
    public bool SendEmail { get; set; }
    [FromForm(Name = "files")]
    public List<IFormFile> Files { get; set; }
    [FromForm(Name = "maximumGrade")]
    public int MaximumGrade { get; set; }
  }

  public class CourseTaskUpdateCommand : ICommand
  {
    public CourseTaskUpdateCommand() { }
    [FromForm(Name = "id")]
    public int Id { get; set; }
    [FromForm(Name = "title")]
    public string Title { get; set; }
    [FromForm(Name = "description")]
    public string Description { get; set; }
    [FromForm(Name = "dueDate")]
    public DateTime DueDate { get; set; }
    [FromForm(Name = "sendEmail")]
    public bool SendEmail { get; set; }
    [FromForm(Name = "files")]
    public List<IFormFile> Files { get; set; }
    [FromForm(Name = "maximumGrade")]
    public int MaximumGrade { get; set; }
    [FromForm(Name = "courseId")]
    public int CourseId { get; set; }
  }

  public class CourseTaskSubmitAttemptCommand : ICommand
  {
    public CourseTaskSubmitAttemptCommand() { }
    [FromForm(Name = "courseTaskId")]
    public int CourseTaskId { get; set; }
    [FromForm(Name = "description")]
    public string Description { get; set; }
    [FromForm(Name = "files")]
    public List<IFormFile> Files { get; set; }
    [FromForm(Name = "courseId")]
    public int CourseId { get; set; }
  }

  public class CourseTaskDeleteCommand : ICommand
  {
    public CourseTaskDeleteCommand() { }
    public int Id { get; set; }
    public int CourseId { get; set; }
  }

  public class CourseTaskGradeAttemptCommand : ICommand
  {
    public CourseTaskGradeAttemptCommand() { }
    public int AttemptId { get; set; }
    public int GradedById { get; set; }
    public int Grade { get; set; }
  }
}