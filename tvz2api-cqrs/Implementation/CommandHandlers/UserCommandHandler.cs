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
using tvz2api_cqrs.Enumerations;

namespace tvz2api_cqrs.Implementation.CommandHandlers
{
  public class UserCommandHandler :
    ICommandHandlerAsync<UserSubscribeCommand>,
    ICommandHandlerAsync<UserUnsubscribeCommand>,
    ICommandHandlerAsync<UserUpdatePersonalInformationCommand>,
    ICommandHandlerAsync<UserUploadPictureCommand, UserProfilePictureDTO>,
    ICommandHandlerAsync<UserUpdateSettingsCommand>,
    ICommandHandlerAsync<UserUpdatePrivilegesCommand>,
    ICommandHandlerAsync<UserUpdateGeneralCommand>,
    ICommandHandlerAsync<UserUpdateBlacklistCommand>
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

      _context.UserCoursePrivilege.Add(new UserCoursePrivilege()
      {
        CourseId = command.CourseId,
        PrivilegeId = (int)PrivilegeEnum.CanCreateNewDiscussion,
        UserId = command.UserId
      });

      await _context.SaveChangesAsync();
    }

    public async Task HandleAsync(UserUnsubscribeCommand command)
    {
      var user = await _context
        .User
        .Include(t => t.CourseTaskAttemptUser)
          .ThenInclude(t => t.CourseTask)
        .Include(t => t.CourseTaskAttemptUser)
          .ThenInclude(t => t.TaskAttemptAttachment)
        .Include(t => t.DiscussionComment)
          .ThenInclude(t => t.Discussion)
        .Include(t => t.Discussion)
        .Include(t => t.ExamAttempt)
          .ThenInclude(t => t.Exam)
        .Include(t => t.UserCoursePrivilege)
        .FirstOrDefaultAsync(x => x.Id == command.UserId);

      var userTaskAttempts = user.CourseTaskAttemptUser.Where(x => x.CourseTask.CourseId == command.CourseId);
      var userTaskAttemptAttachments = new List<TaskAttemptAttachment>();

      userTaskAttempts.ToList().ForEach(x =>
      {
        x.TaskAttemptAttachment.ToList().ForEach(y => userTaskAttemptAttachments.Add(y));
      });

      _context.TaskAttemptAttachment.RemoveRange(userTaskAttemptAttachments);
      _context.CourseTaskAttempt.RemoveRange(userTaskAttempts);


      _context.DiscussionComment.RemoveRange(user.DiscussionComment.Where(x => x.Discussion.CourseId == command.CourseId));
      _context.Discussion.RemoveRange(user.Discussion.Where(x => x.CourseId == command.CourseId));
      _context.ExamAttempt.RemoveRange(user.ExamAttempt.Where(x => x.Exam.CourseId == command.CourseId));
      _context.UserCoursePrivilege.RemoveRange(user.UserCoursePrivilege.Where(x => x.CourseId == command.CourseId));

      var subscription = await _context.Subscription.Where(x => x.CourseId == command.CourseId && x.UserId == command.UserId).FirstOrDefaultAsync();
      _context.Subscription.Remove(subscription);

      await _context.SaveChangesAsync();
    }

    public async Task HandleAsync(UserUpdateSettingsCommand command)
    {
      var settings = await _context.UserSettings.FirstOrDefaultAsync(x => x.UserId == command.UserId);
      settings.DarkMode = command.DarkMode;
      settings.Locale = command.Locale;
      settings.Popups = command.Popups;
      await _context.SaveChangesAsync();
    }

    public async Task HandleAsync(UserUpdatePrivilegesCommand command)
    {
      var user = await _context
        .User
        .Include(t => t.UserCoursePrivilege)
        .FirstOrDefaultAsync(x => x.Id == command.UserId);

      var currentCoursePrivilegeIds = user.UserCoursePrivilege.Select(x => x.CourseId);
      var res = currentCoursePrivilegeIds.Union(command.Courses).Except(currentCoursePrivilegeIds.Intersect(command.Courses)).ToList();

      res.ForEach(x =>
      {
        if (currentCoursePrivilegeIds.Contains(x))
        {
          var privileges = _context.UserCoursePrivilege.Where(y => y.CourseId == x && y.UserId == command.UserId);
          _context.UserCoursePrivilege.RemoveRange(privileges);
        }
        else
        {
          _context.UserCoursePrivilege.Add(new UserCoursePrivilege()
          {
            CourseId = x,
            PrivilegeId = (int)PrivilegeEnum.IsInvolvedWithCourse,
            UserId = command.UserId
          });
        }
      });

      await _context.SaveChangesAsync();
    }

    public async Task HandleAsync(UserUpdateGeneralCommand command)
    {
      var user = await _context
        .User
        .Include(t => t.UserPrivilege)
        .FirstOrDefaultAsync(x => x.Id == command.UserId);

      var currentGeneralPrivileges = user.UserPrivilege.Select(x => x.PrivilegeId);
      var res = currentGeneralPrivileges.Union(command.Privileges).Except(currentGeneralPrivileges.Intersect(command.Privileges)).ToList();

      res.ForEach(x =>
      {
        if (currentGeneralPrivileges.Contains(x))
        {
          var privileges = _context.UserPrivilege.Where(y => y.PrivilegeId == x && y.UserId == command.UserId);
          _context.UserPrivilege.RemoveRange(privileges);
        }
        else
        {
          _context.UserPrivilege.Add(new UserPrivilege()
          {
            PrivilegeId = x,
            UserId = command.UserId
          });
        }
      });

      await _context.SaveChangesAsync();
    }

    public async Task HandleAsync(UserUpdatePersonalInformationCommand command)
    {
      var user = await _context.User.FirstOrDefaultAsync(x => x.Id == command.UserId);
      user.Name = command.Name;
      user.Surname = command.Surname;
      user.Email = command.Email;
      await _context.SaveChangesAsync();
    }

    public async Task HandleAsync(UserUpdateBlacklistCommand command)
    {
      var user = await _context
        .User
        .Include(t => t.Subscription)
        .FirstOrDefaultAsync(x => x.Id == command.UserId);

      user.Subscription.ToList().ForEach(x =>
      {
        x.Blacklisted = command.CourseIds.Contains((int)x.CourseId);
      });

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
        Data = fileBytes,
        Size = fileBytes.Length
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