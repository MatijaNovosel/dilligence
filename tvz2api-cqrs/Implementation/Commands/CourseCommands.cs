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
}