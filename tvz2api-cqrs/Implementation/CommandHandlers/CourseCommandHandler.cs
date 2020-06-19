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
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using tvz2api_cqrs.Custom;
using tvz2api_cqrs.Enumerations;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace tvz2api_cqrs.Implementation.CommandHandlers
{
  public class CourseCommandHandler :
    ICommandHandlerAsync<CourseCreateNewSidebarCommand>,
    ICommandHandlerAsync<CourseDeleteSidebarCommand>,
    ICommandHandlerAsync<CourseCreateDiscussionCommand>,
    ICommandHandlerAsync<CourseDiscussionReplyCommand>,
    ICommandHandlerAsync<CourseDeleteDiscussionCommand>,
    ICommandHandlerAsync<CourseUpdateLandingPageCommand>,
    ICommandHandlerAsync<CourseUpdatePasswordCommand>,
    ICommandHandlerAsync<CourseDeleteCommand>,
    ICommandHandlerAsync<CourseMuteParticipantCommand>,
    ICommandHandlerAsync<CourseKickParticipantCommand>,
    ICommandHandlerAsync<CourseCreateCommand, int>
  {
    private readonly lmsContext _context;
    private readonly IConfiguration _configuration;

    public CourseCommandHandler(lmsContext context, IConfiguration configuration)
    {
      _context = context;
      _configuration = configuration;
    }

    public async Task HandleAsync(CourseCreateNewSidebarCommand command)
    {
      _context.SidebarContent.Add(new SidebarContent()
      {
        CourseId = command.CourseId,
        Title = command.Title
      });
      await _context.SaveChangesAsync();
    }

    public async Task HandleAsync(CourseUpdatePasswordCommand command)
    {
      var course = _context.Course.FirstOrDefault(x => x.Id == command.CourseId);
      course.Password = command.NewPassword;
      await _context.SaveChangesAsync();
    }

    public async Task HandleAsync(CourseDiscussionReplyCommand command)
    {
      _context.DiscussionComment.Add(new DiscussionComment()
      {
        Content = command.Content,
        DiscussionId = command.DiscussionId,
        SubmittedAt = DateTime.Now,
        SubmittedById = command.SubmittedById
      });
      await _context.SaveChangesAsync();
    }

    public async Task HandleAsync(CourseDeleteCommand command)
    {
      var course = await _context
        .Course
        .Include(x => x.Subscription)
        .Include(x => x.CourseTask)
        .ThenInclude(x => x.CourseTaskAttachment)
        .Include(x => x.CourseTask)
        .ThenInclude(x => x.CourseTaskAttempt)
        .ThenInclude(x => x.TaskAttemptAttachment)
        .Include(x => x.Discussion)
        .ThenInclude(x => x.DiscussionComment)
        .Include(x => x.Exam)
        .ThenInclude(x => x.ExamAttempt)
        .ThenInclude(x => x.UserAnswer)
        .Include(x => x.Exam)
        .ThenInclude(x => x.Question)
        .ThenInclude(x => x.Answer)
        .Include(x => x.SidebarContent)
        .ThenInclude(x => x.SidebarContentFile)
        .Include(x => x.Notification)
        .ThenInclude(x => x.NotificationUserSeen)
        .FirstOrDefaultAsync(x => x.Id == command.CourseId);

      _context.Subscription.RemoveRange(course.Subscription);

      var notifications = course.Notification.ToList();
      notifications.ForEach(x =>
      {
        _context.NotificationUserSeen.RemoveRange(x.NotificationUserSeen);
      });

      _context.Notification.RemoveRange(notifications);

      var courseTasks = course.CourseTask.ToList();
      courseTasks.ForEach(x =>
      {
        _context.CourseTaskAttachment.RemoveRange(x.CourseTaskAttachment);
        var attempts = x.CourseTaskAttempt.ToList();
        attempts.ForEach(y =>
        {
          _context.TaskAttemptAttachment.RemoveRange(y.TaskAttemptAttachment);
        });
        _context.CourseTaskAttempt.RemoveRange(attempts);
      });

      var discussions = course.Discussion.ToList();
      discussions.ForEach(x =>
      {
        _context.DiscussionComment.RemoveRange(x.DiscussionComment);
      });
      _context.Discussion.RemoveRange(discussions);

      var exams = course.Exam.ToList();

      exams.ForEach(x =>
      {
        var attempts = x.ExamAttempt.ToList();

        attempts.ForEach(y =>
        {
          _context.UserAnswer.RemoveRange(y.UserAnswer);
        });

        _context.ExamAttempt.RemoveRange(attempts);

        var questions = x.Question.ToList();
        questions.ForEach(y =>
        {
          _context.Answer.RemoveRange(y.Answer);
        });
        _context.Question.RemoveRange(questions);
      });

      _context.Exam.RemoveRange(exams);

      _context.Course.Remove(course);

      await _context.SaveChangesAsync();
    }

    public async Task HandleAsync(CourseMuteParticipantCommand command)
    {
      if (command.Mute == true)
      {
        var privelege = await _context
          .UserCoursePrivilege
          .FirstOrDefaultAsync(x =>
            x.UserId == command.UserId &&
            x.CourseId == command.CourseId &&
            x.PrivilegeId == (int)PrivilegeEnum.CanCreateNewDiscussion
          );
        if (privelege != null)
        {
          _context.UserCoursePrivilege.Remove(privelege);
        }
      }
      else
      {
        _context.UserCoursePrivilege.Add(new UserCoursePrivilege()
        {
          CourseId = command.CourseId,
          PrivilegeId = (int)PrivilegeEnum.CanCreateNewDiscussion,
          UserId = command.UserId
        });
      }

      _context.SaveChanges();
    }

    public async Task HandleAsync(CourseKickParticipantCommand command)
    {
      var user = await _context
        .User
        .Include(x => x.Subscription)
        .Include(x => x.DiscussionComment)
        .Include(x => x.Discussion)
        .ThenInclude(x => x.DiscussionComment)
        .Include(x => x.ExamAttempt)
        .ThenInclude(x => x.UserAnswer)
        .Include(x => x.CourseTaskAttemptUser)
        .ThenInclude(x => x.TaskAttemptAttachment)
        .Include(x => x.UserCoursePrivilege)
        .FirstOrDefaultAsync(x => x.Id == command.UserId);

      _context.Discussion.RemoveRange(user.Discussion);
      _context.DiscussionComment.RemoveRange(user.DiscussionComment);

      var attempts = user.ExamAttempt.ToList();
      attempts.ForEach(x =>
      {
        _context.UserAnswer.RemoveRange(x.UserAnswer);
      });
      _context.ExamAttempt.RemoveRange(user.ExamAttempt);

      var courseTaskAttempts = user.CourseTaskAttemptUser.ToList();
      courseTaskAttempts.ForEach(x =>
      {
        _context.TaskAttemptAttachment.RemoveRange(x.TaskAttemptAttachment);
      });
      _context.RemoveRange(courseTaskAttempts);

      _context.UserCoursePrivilege.RemoveRange(user.UserCoursePrivilege);

      _context.Subscription.RemoveRange(user.Subscription.Where(x => x.CourseId == command.CourseId));

      _context.SaveChanges();
    }

    public async Task<ICommandResult<int>> HandleAsync(CourseCreateCommand command)
    {
      if (_context.Course.Any(x => String.Equals(x.Name.ToLower(), command.Name.ToLower())))
      {
        throw new Exception("This name is already used!");
      }

      var newCourse = new Course()
      {
        Name = command.Name,
        MadeById = command.CreatedById,
        Password = command.Password,
        SpecializationId = command.SpecializationId
      };

      _context.Course.Add(newCourse);
      await _context.SaveChangesAsync();

      var privileges = new List<UserCoursePrivilege>() {
        new UserCoursePrivilege()
        {
          CourseId = newCourse.Id,
          PrivilegeId = (int)PrivilegeEnum.CanManageCourse,
          UserId = command.CreatedById
        },
        new UserCoursePrivilege()
        {
          CourseId = newCourse.Id,
          PrivilegeId = (int)PrivilegeEnum.IsInvolvedWithCourse,
          UserId = command.CreatedById
        }
      };

      _context.Subscription.Add(new Subscription()
      {
        CourseId = newCourse.Id,
        JoinedAt = DateTime.Now,
        UserId = command.CreatedById,
        Blacklisted = false
      });

      _context.UserCoursePrivilege.AddRange(privileges);
      await _context.SaveChangesAsync();

      return CommandResult<int>.Success(newCourse.Id);
    }

    public async Task HandleAsync(CourseUpdateLandingPageCommand command)
    {
      var course = _context.Course.FirstOrDefault(x => x.Id == command.CourseId);
      course.LandingPage = command.Content;
      await _context.SaveChangesAsync();
    }

    public async Task HandleAsync(CourseDeleteDiscussionCommand command)
    {
      var discussion = await _context.Discussion.FirstOrDefaultAsync(x => x.Id == command.DiscussionId);
      _context.Discussion.Remove(discussion);
      await _context.SaveChangesAsync();
    }

    public async Task HandleAsync(CourseCreateDiscussionCommand command)
    {
      var discussion = new Discussion()
      {
        CourseId = command.CourseId,
        Content = command.Body,
        SubmittedAt = DateTime.Now,
        SubmittedById = command.SubmittedById
      };

      _context.Discussion.Add(discussion);
      await _context.SaveChangesAsync();
    }

    public async Task HandleAsync(CourseDeleteSidebarCommand command)
    {
      var sidebar = await _context.SidebarContent.FirstOrDefaultAsync(x => x.Id == command.SidebarId);
      _context.Remove(sidebar);
      await _context.SaveChangesAsync();
    }
  }
}
