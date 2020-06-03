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

  public class CourseDeleteDiscussionCommand : ICommand
  {
    public CourseDeleteDiscussionCommand(int discussionId)
    {
      DiscussionId = discussionId;
    }
    public int DiscussionId { get; set; }   
  }

  public class CourseUpdateLandingPageCommand : ICommand
  {
    public CourseUpdateLandingPageCommand() { }
    public CourseUpdateLandingPageCommand(int courseId, string content)
    {
      CourseId = courseId;
      Content = content;
    }
    public int CourseId { get; set; } 
    public string Content { get; set; } 
  }

  public class CourseDeleteSidebarCommand : ICommand
  {
    public CourseDeleteSidebarCommand() { }
    public int SidebarId { get; set; }
    public int CourseId { get; set; }
  }

  public class CourseDiscussionReplyCommand : ICommand
  {
    public CourseDiscussionReplyCommand() {}
    public int DiscussionId { get; set; }
    public int SubmittedById { get; set; }
    public string Content { get; set; }
  }

  public class CourseCreateDiscussionCommand : ICommand
  {
    public CourseCreateDiscussionCommand() { }

    [FromForm(Name = "courseId")]
    public int CourseId { get; set; }
    [FromForm(Name = "body")]
    public string Body { get; set; }
    [FromForm(Name = "submittedById")]
    public int SubmittedById { get; set; }
  }
}