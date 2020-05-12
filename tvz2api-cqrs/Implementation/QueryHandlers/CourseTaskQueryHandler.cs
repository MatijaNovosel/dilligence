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
    IQueryHandlerAsync<CourseTaskQuery, List<CourseTaskQueryModel>>
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
      var notifications = await _context.CourseTask
        .Include(t => t.CourseTaskAttachment)
        .ThenInclude(t => t.File)
        .Where(t => t.CourseId == query.CourseId)
        .Select(t => new CourseTaskQueryModel
        {
          Id = t.Id,
          SubmittedAt = t.SubmittedAt,
          Title = t.Title,
          CourseId = t.CourseId,
          DueDate = t.DueDate,
          CreatedBy = $"{t.CreatedBy.Name} {t.CreatedBy.Surname}",
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
      return notifications;
    }
  }
}
