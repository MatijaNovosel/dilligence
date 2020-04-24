using tvz2api_cqrs.Models;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Infrastructure.QueryHandlers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using tvz2api_cqrs.Models.DTO;

namespace tvz2api_cqrs.Implementation.QueryHandlers
{
  public class NotificationQueryHandler :
    IQueryHandlerAsync<NotificationQuery, List<NotificationQueryModel>>,
    IQueryHandlerAsync<NotificationUserQuery, List<NotificationQueryModel>>,
    IQueryHandlerAsync<NotificationUserTotalQuery, int>
  {
    private readonly lmsContext _context;

    public NotificationQueryHandler(lmsContext context)
    {
      _context = context;
    }

    public async Task<List<NotificationQueryModel>> HandleAsync(NotificationQuery query)
    {
      var notifications = await _context.Notification
        .Where(t => t.CourseId == query.Id)
        .Select(t => new NotificationQueryModel
        {
          Id = t.Id,
          SubmittedAt = t.SubmittedAt,
          Title = t.Title,
          Course = t.Course.Name,
          SubmittedBy = $"{t.SubmittedBy.Name} {t.SubmittedBy.Surname}",
          Description = t.Description
        })
        .ToListAsync();
      return notifications;
    }

    public async Task<List<NotificationQueryModel>> HandleAsync(NotificationUserQuery query)
    {
      var notifications = await _context.Notification
        .Where(t => t.Course.Subscription.Any(x => x.UserId == query.UserId))
        .Select(t => new NotificationQueryModel
        {
          Id = t.Id,
          SubmittedAt = t.SubmittedAt,
          Title = t.Title,
          Course = t.Course.Name,
          SubmittedBy = $"{t.SubmittedBy.Name} {t.SubmittedBy.Surname}",
          Description = t.Description
        })
        .ToListAsync();
      return notifications;
    }

    public async Task<int> HandleAsync(NotificationUserTotalQuery query)
    {
      var count = await _context.Notification
        .Where(t => t.Course.Subscription.Any(x => x.UserId == query.UserId))
        .Select(t => new NotificationQueryModel
        {
          Id = t.Id,
          SubmittedAt = t.SubmittedAt,
          Title = t.Title,
          Course = t.Course.Name,
          SubmittedBy = $"{t.SubmittedBy.Name} {t.SubmittedBy.Surname}",
          Description = t.Description
        })
        .CountAsync();
      return count;
    }
  }
}
