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
    [FromForm(Name = "submittedById")]
    public int SubmittedById { get; set; }
    [FromForm(Name = "courseId")]
    public int CourseId { get; set; }
    [FromForm(Name = "title")]
    public string Title { get; set; }
    [FromForm(Name = "description")]
    public string Description { get; set; }
    [FromForm(Name = "color")]
    public string Color { get; set; }
    [FromForm(Name = "expiresAt")]
    public DateTime ExpiresAt { get; set; }
    [FromForm(Name = "sendEmail")]
    public bool SendEmail { get; set; }
    [FromForm(Name = "files")]
    public List<IFormFile> Files { get; set; }
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
