using tvz2api_cqrs.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tvz2api_cqrs.QueryModels;

namespace tvz2api_cqrs.Implementation.Commands
{
  public class NotificationCreateCommand : ICommand<NotificationQueryModel>
  {
    public NotificationCreateCommand() { }
    public string Title { get; set; }
    public string Description { get; set; }
    public int? SubmittedById { get; set; }
    public int? CourseId { get; set; }
    public DateTime? SubmittedAt { get; set; }
  }
}
