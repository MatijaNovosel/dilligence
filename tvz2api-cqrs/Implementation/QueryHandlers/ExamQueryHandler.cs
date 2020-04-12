using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.Infrastructure.QueryHandlers;
using tvz2api_cqrs.Models;
using tvz2api_cqrs.Models.DTO;
using tvz2api_cqrs.QueryModels;

namespace tvz2api_cqrs.Implementation.QueryHandlers
{
  public class ExamQueryHandler :
    IQueryHandlerAsync<ExamDetailsQuery, ExamDetailsQueryModel>,
    IQueryHandlerAsync<ExamInProgressQuery, List<ExamAttemptQueryModel>>
  {
    private readonly tvz2Context _context;

    public ExamQueryHandler(tvz2Context context)
    {
      _context = context;
    }

    public async Task<ExamDetailsQueryModel> HandleAsync(ExamDetailsQuery query)
    {
      var exam = await _context.Exam
        .Where(t => t.Id == query.Id)
        .Select(t => new ExamDetailsQueryModel
        {
          Id = t.Id,
          Naziv = t.Naziv,
          SubjectId = t.SubjectId,
          Questions = t.Question
            .Select(x => new QuestionDTO
            {
              Id = x.Id,
              Content = x.Content,
              Title = x.Title,
              TypeId = x.TypeId,
              Answers = x.Answer.Select(y => new AnswerDTO()
              {
                Label = y.Content,
                Value = y.Id
              }).ToList()
            }).ToList()
        })
        .FirstOrDefaultAsync();
      return exam;
    }

    public async Task<List<ExamAttemptQueryModel>> HandleAsync(ExamInProgressQuery query)
    {
      var exams = await _context.ExamAttempt
        .Where(t => t.UserId == query.UserId)
        .Select(t => new ExamAttemptQueryModel
        {
          Id = t.Id,
          TimeLeft = t.TimeLeft,
          Terminated = t.Terminated,
          Exam = new ExamDTO()
          {
            Id = t.Exam.Id,
            Naziv = t.Exam.Naziv,
            Subject = t.Exam.Subject.Naziv
          }
        }).ToListAsync();
      return exams;
    }
  }
}