using tvz2api_cqrs.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tvz2api_cqrs.QueryModels;

namespace tvz2api_cqrs.Implementation.Commands
{
  public class NotificationCreateCommand : ICommand
  {
    public NotificationCreateCommand() { }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Color { get; set; }
    public int? SubmittedById { get; set; }
    public int? CourseId { get; set; }
    public bool SendEmail { get; set; }
    public DateTime? ExpiresAt { get; set; }
  }

  public class NotificationSeenCommand : ICommand
  {
    public NotificationSeenCommand() { }
    public int? UserId { get; set; }
    public List<int> NotificationIds { get; set; }
  }

  public class NotificationDeleteCommand : ICommand
  {
    public NotificationDeleteCommand(int id)
    {
      Id = id;
    }
    public int? Id { get; set; }
  }
}
