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
        .Where(t => t.CourseId == query.CourseId)
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
      // Filter condition: retrieve the notification if it has not been seen by the user and if it has been submitted after the user had joined the course
      var notifications = await _context.Notification
        .Where(t =>
          t.Course.Subscription.Any(x => x.UserId == query.UserId && x.JoinedAt < t.SubmittedAt) &&
          t.NotificationUserSeen.Any(x => x.UserId == query.UserId && x.NotificationId == t.Id && x.Seen == false)
        )
        .Select(t => new NotificationQueryModel
        {
          Id = t.Id,
          SubmittedAt = t.SubmittedAt,
          Title = t.Title,
          Course = t.Course.Name,
          SubmittedBy = $"{t.SubmittedBy.Name} {t.SubmittedBy.Surname}",
          Description = t.Description,
          CourseId = t.Course.Id
        })
        .ToListAsync();
      return notifications;
    }

    public async Task<int> HandleAsync(NotificationUserTotalQuery query)
    {
      var notifications = await _context.Notification
        .Where(t =>
          t.Course.Subscription.Any(x => x.UserId == query.UserId && x.JoinedAt < t.SubmittedAt) &&
          t.NotificationUserSeen.Any(x => x.UserId == query.UserId && x.NotificationId == t.Id && x.Seen == false)
        )
        .ToListAsync();
      return notifications.Count;
    }
  }
}
