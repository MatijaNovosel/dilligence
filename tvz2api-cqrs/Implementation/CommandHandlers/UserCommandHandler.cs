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
using tvz2api_cqrs.Models.DTO;
using tvz2api_cqrs.QueryModels;

namespace tvz2api_cqrs.Implementation.CommandHandlers
{
  public class UserCommandHandler :
    ICommandHandlerAsync<UserSubscribeCommand>,
    ICommandHandlerAsync<UserUnsubscribeCommand>,
    ICommandHandlerAsync<UserUploadPictureCommand, UserProfilePictureDTO>,
    ICommandHandlerAsync<UserUpdateSettingsCommand>
  {
    private readonly lmsContext _context;

    public UserCommandHandler(lmsContext context)
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
        UserId = command.UserId,
        JoinedAt = DateTime.Now,
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

    public async Task<ICommandResult<UserProfilePictureDTO>> HandleAsync(UserUploadPictureCommand command)
    {
      var picture = _context.User.FirstOrDefault(x => x.Id == command.UserId).ImageFile;
      if (picture != null)
      {
        _context.File.Remove(picture);
      }

      var ms = new MemoryStream();

      await command.Picture.CopyToAsync(ms);
      var fileBytes = ms.ToArray();

      var fileName = ContentDispositionHeaderValue.Parse(command.Picture.ContentDisposition).FileName.Trim('"');
      fileName = fileName.Substring(0, fileName.LastIndexOf(".")) + fileName.Substring(fileName.LastIndexOf(".")).ToLower();

      Models.File file = new Models.File()
      {
        Name = Path.GetFileName(fileName),
        ContentType = command.Picture.ContentType,
        Data = fileBytes
      };

      await _context.File.AddAsync(file);
      await _context.SaveChangesAsync();

      await ms.DisposeAsync();

      _context.User.FirstOrDefault(x => x.Id == command.UserId).ImageFileId = file.Id;

      await _context.SaveChangesAsync();

      return CommandResult<UserProfilePictureDTO>.Success(new UserProfilePictureDTO() { Picture = file });
    }
  }
}