using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tvz2api_cqrs.Infrastructure.Commands;
using tvz2api_cqrs.Models.DTO;

namespace tvz2api_cqrs.Implementation.Commands
{
  public class CourseCreateNewSidebarCommand : ICommand
  {
    public CourseCreateNewSidebarCommand() { }
    public int CourseId { get; set; }
    public string Title { get; set; }
  }

  public class CourseDeleteSidebarCommand : ICommand
  {
    public CourseDeleteSidebarCommand() { }
    public int SidebarId { get; set; }
    public int CourseId { get; set; }
  }

  public class CourseCreateDiscussionCommand : ICommand
  {
    public CourseCreateDiscussionCommand() { }

    [FromForm(Name = "courseId")]
    public int CourseId { get; set; }
    [FromForm(Name = "body")]
    public string Body { get; set; }
    [FromForm(Name = "backgroundColor")]
    public string BackgroundColor { get; set; }
    [FromForm(Name = "textColor")]
    public string TextColor { get; set; }
    [FromForm(Name = "attachments")]
    public List<IFormFile> Attachments { get; set; }
    [FromForm(Name = "images")]
    public List<IFormFile> Images { get; set; }
    [FromForm(Name = "submittedById")]
    public int SubmittedById { get; set; }
  }
}