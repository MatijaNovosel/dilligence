using tvz2api_cqrs.Common;
using tvz2api_cqrs.Implementation.Commands;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Models;
using tvz2api_cqrs.Infrastructure.CommandHandlers;
using tvz2api_cqrs.Infrastructure.Commands;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace tvz2api_cqrs.Implementation.CommandHandlers
{
  public class NotificationCommandHandler :
    ICommandHandlerAsync<NotificationCreateCommand>,
    ICommandHandlerAsync<NotificationSeenCommand>
  {
    private readonly lmsContext _context;

    public NotificationCommandHandler(lmsContext context)
    {
      _context = context;
    }

    public async Task HandleAsync(NotificationCreateCommand command)
    {
      Notification notification = new Notification()
      {
        CourseId = command.CourseId,
        SubmittedById = command.SubmittedById,
        Title = command.Title,
        Description = command.Description,
        SubmittedAt = DateTime.Now
      };
      await _context.Notification.AddAsync(notification);
      await _context.SaveChangesAsync();

      var course = _context.Course.Include(t => t.Subscription).FirstOrDefault(t => t.Id == command.CourseId);
      var subscriptionUserIds = course.Subscription.Select(t => t.UserId).ToList();

      subscriptionUserIds.ForEach(x =>
      {
        _context.NotificationUserSeen.Add(new NotificationUserSeen()
        {
          NotificationId = notification.Id,
          UserId = x
        });
      });

      await _context.SaveChangesAsync();
    }

    public async Task HandleAsync(NotificationSeenCommand command)
    {
      command.NotificationIds.ForEach(x =>
      {
        _context.NotificationUserSeen
          .FirstOrDefault(t => t.UserId == command.UserId && t.NotificationId == x)
          .Seen = true;
      });
      await _context.SaveChangesAsync();
    }
  }
}
