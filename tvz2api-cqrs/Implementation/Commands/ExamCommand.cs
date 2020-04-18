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
    public int TimeLeft { get; set; }
    public UpdateExamDetailsDTO Exam { get; set; }
  }

  public class ExamStartAttemptCommand : ICommand
  {
    public ExamStartAttemptCommand() { }
    public int AttemptId { get; set; }
  }

  public class ExamCreateCommand : ICommand
  {
    public ExamCreateCommand() { }
    public string Naziv { get; set; }
    public int TimeNeeded { get; set; }
    public DateTime DueDate { get; set; }
    public List<CreateQuestionDTO> Questions { get; set; }
  }
}
