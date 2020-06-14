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
    IQueryHandlerAsync<ExamInProgressDetailsQuery, ExamAttemptDetailsQueryModel>,
    IQueryHandlerAsync<ExamInProgressQuery, List<ExamAttemptQueryModel>>,
    IQueryHandlerAsync<ExamUnfinishedQuery, List<UnfinishedExamDTO>>,
    IQueryHandlerAsync<ExamFinishedQuery, List<FinishedExamDTO>>,
    IQueryHandlerAsync<ExamUnfinishedDetailsQuery, ExamUnfinishedDetailsQueryModel>,
    IQueryHandlerAsync<ExamPreviewQuery, ExamPreviewQueryModel>,
    IQueryHandlerAsync<ExamAvailableQuery, List<FinishedExamDTO>>,
    IQueryHandlerAsync<ExamInProgressCourseQuery, List<ExamInProgressDTO>>
  {
    private readonly lmsContext _context;

    public ExamQueryHandler(lmsContext context)
    {
      _context = context;
    }

    public async Task<ExamAttemptDetailsQueryModel> HandleAsync(ExamInProgressDetailsQuery query)
    {
      var exam = await _context.ExamAttempt
        .Where(t => t.Id == query.Id)
        .Select(t => new ExamAttemptDetailsQueryModel
        {
          Id = t.Id,
          Terminated = t.Terminated,
          StartedAt = t.StartedAt,
          CourseId = (int)t.Exam.CourseId,
          Exam = new ExamDetailsDTO()
          {
            Id = t.Exam.Id,
            Name = t.Exam.Name,
            CreatedBy = t.Exam.CreatedBy.Username,
            DueDate = t.Exam.DueDate,
            TimeNeeded = t.Exam.TimeNeeded,
            Questions = t.Exam.Question
            .Select(x => new QuestionDTO
            {
              Id = x.Id,
              Content = x.Content,
              Title = x.Title,
              TypeId = x.TypeId,
              Value = x.Value,
              UserAnswers = x.UserAnswer
                .Where(y => y.QuestionId == x.Id && y.AttemptId == query.Id)
                .Select(y => (int)y.AnswerId)
                .ToList(),
              Answers = x.Answer.Select(y => new AnswerDTO()
              {
                Label = y.Content,
                Value = y.Id
              }).ToList()
            }).ToList()
          }
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
          StartedAt = t.StartedAt,
          Started = t.Started,
          Terminated = t.Terminated,
          Exam = new ExamDTO()
          {
            Id = t.Exam.Id,
            Name = t.Exam.Name,
            TimeNeeded = t.Exam.TimeNeeded,
            Subject = t.Exam.Course.Name
          }
        }).ToListAsync();
      return exams;
    }

    public async Task<List<UnfinishedExamDTO>> HandleAsync(ExamUnfinishedQuery query)
    {
      var exams = await _context.Exam
        .Where(t => t.CreatedById == query.UserId && t.Finalized == false)
        .Select(t => new UnfinishedExamDTO
        {
          CourseId = t.CourseId,
          Id = t.Id,
          CreatedById = t.CreatedById
        }).ToListAsync();
      return exams;
    }

    public async Task<List<FinishedExamDTO>> HandleAsync(ExamFinishedQuery query)
    {
      var exams = await _context.Exam
        .Where(t => t.CreatedById == query.UserId && t.Finalized == true)
        .Select(t => new FinishedExamDTO
        {
          CourseId = t.CourseId,
          Id = t.Id,
          CreatedById = t.CreatedById,
          Enabled = (bool)t.Enabled,
          Name = t.Name
        }).ToListAsync();
      return exams;
    }

    public async Task<List<FinishedExamDTO>> HandleAsync(ExamAvailableQuery query)
    {
      var exams = await _context.Exam
        .Include(t => t.ExamAttempt)
        .Where(t => t.CourseId == query.CourseId && t.Finalized == true && !t.ExamAttempt.Any(x => x.UserId == query.UserId))
        .Select(t => new FinishedExamDTO
        {
          CourseId = t.CourseId,
          Id = t.Id,
          CreatedById = t.CreatedById,
          Enabled = (bool)t.Enabled,
          Name = t.Name
        }).ToListAsync();
      return exams;
    }

    public async Task<List<ExamInProgressDTO>> HandleAsync(ExamInProgressCourseQuery query)
    {
      var exams = await _context
        .ExamAttempt
        .Include(t => t.Exam)
        .Where(t => t.Exam.CourseId == query.CourseId && t.UserId == query.UserId)
        .Select(t => new ExamInProgressDTO
        {
          CourseId = t.Exam.CourseId,
          Id = t.Id,
          CreatedById = t.Exam.CreatedById,
          Enabled = (bool)t.Exam.Enabled,
          Name = t.Exam.Name,
          TimeNeeded = t.Exam.TimeNeeded,
          StartedAt = t.StartedAt
        }).ToListAsync();
      return exams;
    }

    public async Task<ExamUnfinishedDetailsQueryModel> HandleAsync(ExamUnfinishedDetailsQuery query)
    {
      var unfinishedExam = await _context.Exam
        .Include(x => x.CreatedBy)
        .Include(x => x.Question)
        .ThenInclude(x => x.Answer)
        .Where(x => x.Id == query.Id)
        .Select(x => new ExamUnfinishedDetailsQueryModel()
        {
          CreatedById = (int)x.CreatedById,
          CreatedBy = $"{x.CreatedBy.Name} {x.CreatedBy.Surname}",
          DueDate = x.DueDate,
          Name = x.Name,
          CourseId = (int)x.CourseId,
          TimeNeeded = x.TimeNeeded,
          Id = x.Id,
          Questions = x.Question.Select(y => new CreateQuestionDTO()
          {
            Content = y.Content,
            Title = y.Title,
            TypeId = y.TypeId,
            Value = y.Value,
            Answers = y.Answer.Select(z => new CreateAnswerDTO()
            {
              Content = z.Content,
              Correct = (bool)z.Correct
            }).ToList()
          }).ToList()
        }).FirstOrDefaultAsync();
      return unfinishedExam;
    }

    public async Task<ExamPreviewQueryModel> HandleAsync(ExamPreviewQuery query)
    {
      var exam = await _context.Exam
        .Include(x => x.CreatedBy)
        .Include(x => x.Question)
        .ThenInclude(x => x.Answer)
        .Where(x => x.Id == query.ExamId)
        .Select(x => new ExamPreviewQueryModel()
        {
          CreatedById = (int)x.CreatedById,
          CreatedBy = $"{x.CreatedBy.Name} {x.CreatedBy.Surname}",
          DueDate = x.DueDate,
          Name = x.Name,
          CourseId = (int)x.CourseId,
          TimeNeeded = x.TimeNeeded,
          Id = x.Id,
          Questions = x.Question.Select(y => new QuestionDTO()
          {
            Content = y.Content,
            Title = y.Title,
            TypeId = y.TypeId,
            Value = y.Value,
            Answers = y.Answer.Select(z => new AnswerDTO()
            {
              Label = z.Content,
              Value = z.Id
            }).ToList()
          }).ToList()
        }).FirstOrDefaultAsync();
      return exam;
    }
  }
}