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

namespace tvz2api_cqrs.Implementation.CommandHandlers
{
  public class NotificationCommandHandler :
    ICommandHandlerAsync<NotificationCreateCommand>,
    ICommandHandlerAsync<NotificationSeenCommand>,
    ICommandHandlerAsync<NotificationDeleteCommand>
  {
    private readonly lmsContext _context;
    private readonly IConfiguration _configuration;

    public NotificationCommandHandler(lmsContext context, IConfiguration configuration)
    {
      _context = context;
      _configuration = configuration;
    }

    public async Task HandleAsync(NotificationCreateCommand command)
    {
      Notification notification = new Notification()
      {
        CourseId = command.CourseId,
        SubmittedById = command.SubmittedById,
        Title = command.Title,
        Description = command.Description,
        SubmittedAt = DateTime.Now,
        Color = command.Color,
        ExpiresAt = command.ExpiresAt
      };
      await _context.Notification.AddAsync(notification);
      await _context.SaveChangesAsync();

      var course = _context
        .Course
        .Include(t => t.Subscription)
        .ThenInclude(x => x.User)
        .ThenInclude(x => x.UserNotificationBlacklist)
        .FirstOrDefault(t => t.Id == command.CourseId);

      course.Subscription.Where(t => t.UserId != command.SubmittedById).Select(t => t.UserId).ToList().ForEach(x =>
      {
        _context.NotificationUserSeen.Add(new NotificationUserSeen()
        {
          NotificationId = notification.Id,
          UserId = x
        });
      });

      // Send the emails, if the users are receiving notifications from the course
      if (command.SendEmail == true)
      {
        using (SmtpClient client = new SmtpClient()
        {
          UseDefaultCredentials = false,
          Credentials = new System.Net.NetworkCredential("mnovosel2@tvz.hr", _configuration.GetValue<string>("Secrets:EmailPassword")),
          Port = 587,
          Host = "smtp.office365.com",
          DeliveryMethod = SmtpDeliveryMethod.Network,
          TargetName = "STARTTLS/smtp.office365.com",
          EnableSsl = true
        })
        {
          MailMessage message = new MailMessage();
          course.Subscription.Select(x => x.User).Where(x => x.Id != command.SubmittedById).ToList().ForEach(x =>
          {
            if (x.UserNotificationBlacklist.FirstOrDefault(y => y.CourseId == command.CourseId && y.UserId == x.Id) != null)
            {
              message.To.Add(x.Email);
            }
          });
          message.From = new MailAddress(_context.User.FirstOrDefault(x => x.Id == command.SubmittedById).Email);
          message.Subject = command.Title;
          message.Body = command.Description;
          message.IsBodyHtml = true;
          client.Send(message);
        }
      }

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

    public async Task HandleAsync(NotificationDeleteCommand command)
    {
      var notification = await _context.Notification.Include(t => t.NotificationUserSeen).FirstOrDefaultAsync();
      _context.NotificationUserSeen.RemoveRange(notification.NotificationUserSeen);
      _context.Notification.Remove(notification);
      await _context.SaveChangesAsync();
    }
  }
}
