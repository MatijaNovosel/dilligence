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
    public UserSubscribeCommand(int userId, string password, int kolegijId)
    {
      UserId = userId;
      Password = password;
      KolegijId = kolegijId;
    }
    public int UserId { get; set; }
    public string Password { get; set; }
    public int KolegijId { get; set; }
  }

  public class UserUnsubscribeCommand : ICommand
  {
    public UserUnsubscribeCommand() { }
    public UserUnsubscribeCommand(int userId, int kolegijId)
    {
      UserId = userId;
      KolegijId = kolegijId;
    }
    public int UserId { get; set; }
    public int KolegijId { get; set; }
  }

  public class UserUpdateSettingsCommand : ICommand
  {
    public UserUpdateSettingsCommand() { }
    public int UserId { get; set; }
    public bool DarkMode { get; set; }
    public string Locale { get; set; }
  }
}