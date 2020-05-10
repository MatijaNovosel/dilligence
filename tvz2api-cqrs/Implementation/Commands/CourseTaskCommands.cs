using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tvz2api_cqrs.Infrastructure.Commands;
using tvz2api_cqrs.Models.DTO;

namespace tvz2api_cqrs.Implementation.Commands
{
  public class CourseTaskCreateCommand : ICommand
  {
    public CourseTaskCreateCommand() { }
    [FromForm(Name = "createdById")]
    public int CreatedById { get; set; }
    [FromForm(Name = "courseId")]
    public int CourseId { get; set; }
    [FromForm(Name = "title")]
    public string Title { get; set; }
    [FromForm(Name = "description")]
    public string Description { get; set; }
    [FromForm(Name = "dueDate")]
    public DateTime DueDate { get; set; }
    [FromForm(Name = "sendEmail")]
    public bool SendEmail { get; set; }
    [FromForm(Name = "files")]
    public List<IFormFile> Files { get; set; }
  }
}