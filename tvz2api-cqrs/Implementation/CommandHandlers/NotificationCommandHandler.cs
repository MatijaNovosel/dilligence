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
    ICommandHandlerAsync<NotificationCreateCommand, NotificationQueryModel>
  {
    private readonly tvz2Context _context;

    public NotificationCommandHandler(tvz2Context context)
    {
      _context = context;
    }

    public async Task<ICommandResult<NotificationQueryModel>> HandleAsync(NotificationCreateCommand command)
    {
      Notification vijest = new Notification()
      {
        CourseId = 147,
        SubmittedById = 1,
        Title = command.Title,
        Description = "Hardkodirani opis",
        SubmittedAt = DateTime.Now
      };
      await _context.Notification.AddAsync(vijest);
      await _context.SaveChangesAsync();
      return CommandResult<NotificationQueryModel>.Success(new NotificationQueryModel()
      {
        Id = vijest.Id,
        CourseId = vijest.CourseId,
        SubmittedById = vijest.SubmittedById,
        Title = vijest.Title,
        Description = vijest.Description,
        SubmittedAt = vijest.SubmittedAt
      });
    }
  }
}
