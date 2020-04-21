using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tvz2api_cqrs.Infrastructure.Commands;

namespace tvz2api_cqrs.Implementation.Commands
{
  public class UserSubscribeCommand : ICommand
  {
    public UserSubscribeCommand() { }
    public UserSubscribeCommand(int userId, string password, int courseId)
    {
      UserId = userId;
      Password = password;
      CourseId = courseId;
    }
    public int UserId { get; set; }
    public string Password { get; set; }
    public int CourseId { get; set; }
  }

  public class UserUnsubscribeCommand : ICommand
  {
    public UserUnsubscribeCommand() { }
    public UserUnsubscribeCommand(int userId, int courseId)
    {
      UserId = userId;
      CourseId = courseId;
    }
    public int UserId { get; set; }
    public int CourseId { get; set; }
  }

  public class UserUpdateSettingsCommand : ICommand
  {
    public UserUpdateSettingsCommand() { }
    public int UserId { get; set; }
    public bool DarkMode { get; set; }
    public string Locale { get; set; }
  }
}