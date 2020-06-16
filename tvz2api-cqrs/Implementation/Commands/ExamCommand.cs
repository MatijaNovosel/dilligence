using tvz2api_cqrs.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Models.DTO;

namespace tvz2api_cqrs.Implementation.Commands
{
  public class ExamUpdateAttemptCommand : ICommand
  {
    public ExamUpdateAttemptCommand() { }
    public int Id { get; set; }
    public bool Terminated { get; set; }
    public UpdateAttemptDTO Exam { get; set; }
  }

  public class ExamFinalizeCommand : ICommand
  {
    public int ExamId { get; set; }
  }

  public class ExamFinishAttemptCommand : ICommand
  {
    public int AttemptId { get; set; }
  }

  public class ExamStartAttemptCommand : ICommand<int>
  {
    public ExamStartAttemptCommand() { }
    public int ExamId { get; set; }
    public int UserId { get; set; }
  }

  public class ExamEnableSolvingCommand : ICommand
  {
    ExamEnableSolvingCommand() { }
    public int ExamId { get; set; }
  }

  public class ExamUpdateCommand : ICommand
  {
    public ExamUpdateCommand() { }
    public int Id { get; set; }
    public string Name { get; set; }
    public int TimeNeeded { get; set; }
    public DateTime DueDate { get; set; }
    public List<CreateQuestionDTO> Questions { get; set; }
  }

  public class ExamPreCreateCommand : ICommand<int>
  {
    public ExamPreCreateCommand() { }
    public int CreatedById { get; set; }
    public int CourseId { get; set; }
  }
}
