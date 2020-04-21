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
using tvz2api_cqrs.Models;
using tvz2api_cqrs.QueryModels;

namespace tvz2api_cqrs.Implementation.CommandHandlers
{
  public class UserCommandHandler :
    ICommandHandlerAsync<UserSubscribeCommand>,
    ICommandHandlerAsync<UserUnsubscribeCommand>,
    ICommandHandlerAsync<UserUpdateSettingsCommand>
  {
    private readonly tvz2Context _context;

    public UserCommandHandler(tvz2Context context)
    {
      _context = context;
    }

    public async Task HandleAsync(UserSubscribeCommand command)
    {
      if (_context.Course.Where(x => x.Id == command.CourseId).FirstOrDefault().Password != command.Password)
      {
        throw new Exception("Wrong password!");
      }
      await _context.Subscription.AddAsync(new Subscription()
      {
        CourseId = command.CourseId,
        UserId = command.UserId
      });
      await _context.SaveChangesAsync();
    }

    public async Task HandleAsync(UserUnsubscribeCommand command)
    {
      var subscriptiom = await _context.Subscription.Where(x => x.CourseId == command.CourseId && x.UserId == command.UserId).FirstOrDefaultAsync();
      _context.Subscription.Remove(subscriptiom);
      await _context.SaveChangesAsync();
    }

    public async Task HandleAsync(UserUpdateSettingsCommand command)
    {
      var settings = await _context.UserSettings.FirstOrDefaultAsync(x => x.UserId == command.UserId);
      settings.DarkMode = command.DarkMode;
      settings.Locale = command.Locale;
      await _context.SaveChangesAsync();
    }
  }
}