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
    ICommandHandlerAsync<NotificationCreateCommand>
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

      notification.Course.Subscription.Select(x => x.UserId).ToList().ForEach(x =>
      {
        _context.NotificationUserSeen.AddAsync(new NotificationUserSeen()
        {
          NotificationId = notification.Id,
          UserId = x
        });
      });

      await _context.SaveChangesAsync();
    }
  }
}
