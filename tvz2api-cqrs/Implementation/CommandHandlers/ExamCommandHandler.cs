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
    ICommandHandlerAsync<UpdateAttemptCommand>
  {
    private readonly tvz2Context _context;

    public ExamCommandHandler(tvz2Context context)
    {
      _context = context;
    }

    public async Task HandleAsync(UpdateAttemptCommand command)
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