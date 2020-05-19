using tvz2api_cqrs.Models;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Infrastructure.QueryHandlers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using tvz2api_cqrs.Models.DTO;
using System.Security.Claims;
using System;
using tvz2api_cqrs.Custom;

namespace tvz2api_cqrs.Implementation.QueryHandlers
{
  public class CourseTaskQueryHandler :
    IQueryHandlerAsync<CourseTaskQuery, List<CourseTaskQueryModel>>,
    IQueryHandlerAsync<CourseTaskTotalQuery, int>,
    IQueryHandlerAsync<CourseTaskDetailsQuery, CourseTaskQueryModel>,
    IQueryHandlerAsync<CourseTaskAttemptsQuery, List<CourseTaskAttemptQueryModel>>,
    IQueryHandlerAsync<CourseTaskAttemptDetailsQuery, CourseTaskAttemptDetailsQueryModel>
  {
    private readonly lmsContext _context;
    private readonly IUserResolver _userResolver;

    public CourseTaskQueryHandler(lmsContext context, IUserResolver userResolver)
    {
      _context = context;
      _userResolver = userResolver;
    }

    public async Task<List<CourseTaskQueryModel>> HandleAsync(CourseTaskQuery query)
    {
      var courseTasks = await _context.CourseTask
        .Include(t => t.CourseTaskAttachment)
        .ThenInclude(t => t.File)
        .Where(query.Specification.Predicate)
        .Select(t => new CourseTaskQueryModel
        {
          Id = t.Id,
          SubmittedAt = t.SubmittedAt,
          Title = t.Title,
          CourseId = t.CourseId,
          DueDate = t.DueDate,
          CreatedBy = $"{t.CreatedBy.Name} {t.CreatedBy.Surname}",
          CreatedById = t.CreatedById,
          Description = t.Description,
          MaximumGrade = t.GradeMaximum,
          Attachments = t.CourseTaskAttachment.Select(x => new FileDTO()
          {
            ContentType = x.File.ContentType,
            Data = x.File.Data,
            Id = x.File.Id,
            Name = x.File.Name,
            Size = x.File.Size
          }).ToList()
        })
        .ToListAsync();
      return courseTasks;
    }

    public async Task<List<CourseTaskAttemptQueryModel>> HandleAsync(CourseTaskAttemptsQuery query)
    {
      var attempts = await _context
        .CourseTaskAttempt
        .Include(t => t.User)
        .Where(t => t.CourseTaskId == query.Id)
        .Select(t => new CourseTaskAttemptQueryModel()
        {
          Id = t.Id,
          CourseTaskId = t.CourseTaskId,
          Grade = t.Grade,
          GradedBy = $"{t.GradedBy.Name} {t.GradedBy.Surname}",
          GradedById = t.GradedById,
          SubmittedBy = $"{t.User.Name} {t.User.Surname}",
          SubmittedAt = t.SubmittedAt
        })
        .ToListAsync();
      return attempts;
    }

    public async Task<CourseTaskAttemptDetailsQueryModel> HandleAsync(CourseTaskAttemptDetailsQuery query)
    {
      var attempt = await _context
        .CourseTaskAttempt
        .Include(t => t.CourseTask)
        .Include(t => t.TaskAttemptAttachment)
        .ThenInclude(t => t.File)
        .Include(t => t.User)
        .Where(t => t.Id == query.Id)
        .Select(t => new CourseTaskAttemptDetailsQueryModel()
        {
          CourseTaskId = t.CourseTaskId,
          Description = t.Description,
          Grade = t.Grade,
          GradedBy = $"{t.GradedBy.Name} {t.GradedBy.Surname}",
          GradedById = t.GradedById,
          SubmittedBy = $"{t.User.Name} {t.User.Surname}",
          MaximumGrade = t.CourseTask.GradeMaximum,
          GradeeComment = t.GradeeComment,
          Attachments = t.TaskAttemptAttachment.Select(y => new FileDTO()
          {
            ContentType = y.File.ContentType,
            Data = y.File.Data,
            Id = y.File.Id,
            Name = y.File.Name,
            Size = y.File.Size
          })
          .ToList()
        })
        .FirstOrDefaultAsync();
      return attempt;
    }

    public async Task<int> HandleAsync(CourseTaskTotalQuery query)
    {
      var courseTasks = await _context.CourseTask
        .Where(query.Specification.Predicate)
        .CountAsync();
      return courseTasks;
    }
    public async Task<CourseTaskQueryModel> HandleAsync(CourseTaskDetailsQuery query)
    {
      var courseTask = await _context.CourseTask
        .Include(t => t.CourseTaskAttachment)
        .ThenInclude(t => t.File)
        .Where(t => t.Id == query.Id)
        .Select(t => new CourseTaskQueryModel
        {
          Id = t.Id,
          SubmittedAt = t.SubmittedAt,
          Title = t.Title,
          CourseId = t.CourseId,
          DueDate = t.DueDate,
          CreatedBy = $"{t.CreatedBy.Name} {t.CreatedBy.Surname}",
          CreatedById = t.CreatedById,
          Description = t.Description,
          MaximumGrade = t.GradeMaximum,
          Attachments = t.CourseTaskAttachment.Select(x => new FileDTO()
          {
            ContentType = x.File.ContentType,
            Data = x.File.Data,
            Id = x.File.Id,
            Name = x.File.Name,
            Size = x.File.Size
          }).ToList()
        })
        .FirstOrDefaultAsync();
      return courseTask;
    }
  }
}
