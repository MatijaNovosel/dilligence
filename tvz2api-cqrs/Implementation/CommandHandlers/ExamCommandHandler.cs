using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using tvz2api_cqrs.Common;
using tvz2api_cqrs.Implementation.Commands;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.Infrastructure.CommandHandlers;
using tvz2api_cqrs.Infrastructure.Commands;
using tvz2api_cqrs.Enumerations;
using tvz2api_cqrs.Models;
using tvz2api_cqrs.QueryModels;

namespace tvz2api_cqrs.Implementation.CommandHandlers
{
  public class ExamCommandHandler :
    ICommandHandlerAsync<ExamUpdateAttemptCommand>,
    ICommandHandlerAsync<ExamStartAttemptCommand>,
    ICommandHandlerAsync<ExamPreCreateCommand, int>,
    ICommandHandlerAsync<ExamUpdateCommand>
  {
    private readonly lmsContext _context;

    public ExamCommandHandler(lmsContext context)
    {
      _context = context;
    }

    public async Task HandleAsync(ExamStartAttemptCommand command)
    {
      var attempt = await _context.ExamAttempt.FirstOrDefaultAsync(x => x.Id == command.AttemptId);

      attempt.Started = true;
      attempt.StartedAt = DateTime.Now;

      await _context.SaveChangesAsync();
    }

    public async Task HandleAsync(ExamUpdateCommand command)
    {
      var exam = await _context.Exam.FirstOrDefaultAsync(t => t.Id == command.Id);

      exam.Name = command.Name;
      exam.TimeNeeded = command.TimeNeeded;
      exam.DueDate = command.DueDate;
      exam.Finalized = true;

      await _context.Exam.AddAsync(exam);
      await _context.SaveChangesAsync();

      command.Questions.ForEach(x =>
      {
        var question = new Question()
        {
          Content = x.Content,
          Title = x.Title,
          TypeId = x.TypeId,
          ExamId = exam.Id
        };
        _context.Question.Add(question);
        _context.SaveChanges();
        x.Answers.ForEach(y =>
        {
          _context.Answer.Add(new Answer()
          {
            Content = y.Content,
            Correct = y.Correct,
            QuestionId = question.Id
          });
        });
        _context.SaveChanges();
      });
    }

    public async Task<ICommandResult<int>> HandleAsync(ExamPreCreateCommand command)
    {
      var exam = new Exam()
      {
        CreatedById = command.CreatedById,
        CourseId = command.CourseId
      };
      await _context.Exam.AddAsync(exam);
      await _context.SaveChangesAsync();
      return CommandResult<int>.Success(exam.Id);
    }

    public async Task HandleAsync(ExamUpdateAttemptCommand command)
    {
      var attempt = await _context.ExamAttempt.FirstOrDefaultAsync(x => x.Id == command.Id);
      attempt.Terminated = command.Terminated;

      command.Exam.Questions.ForEach(x =>
      {
        if (x.TypeId == (int)QuestionTypeEnum.RADIO)
        {
          var specificUserAnswer = _context.UserAnswer.Where(y => y.QuestionId == x.Id && y.AttemptId == command.Id).FirstOrDefault();
          if (specificUserAnswer == null)
          {
            _context.UserAnswer.Add(new UserAnswer()
            {
              AnswerId = x.UserAnswers.Count != 0 ? (int?)x.UserAnswers[0] : null,
              AttemptId = command.Id,
              QuestionId = x.Id
            });
            return;
          }
          specificUserAnswer.AnswerId = x.UserAnswers.Count != 0 ? (int?)x.UserAnswers[0] : null;
        }
        else
        {
          var specificUserAnswers = _context.UserAnswer.Where(y => y.QuestionId == x.Id && y.AttemptId == command.Id).ToList();
          _context.RemoveRange(specificUserAnswers);
          _context.SaveChanges();
          foreach (var userAnswer in x.UserAnswers)
          {
            _context.UserAnswer.Add(new UserAnswer()
            {
              AnswerId = userAnswer,
              AttemptId = command.Id,
              QuestionId = x.Id
            });
          }
        }
      });

      await _context.SaveChangesAsync();
    }
  }
}