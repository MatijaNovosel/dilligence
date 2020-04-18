using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tvz2api_cqrs.Infrastructure.Commands;

namespace tvz2api_cqrs.Implementation.Commands
{
  public class KorisnikSubscribeCommand : ICommand
  {
    public KorisnikSubscribeCommand() { }
    public KorisnikSubscribeCommand(int userId, string password, int kolegijId)
    {
      UserId = userId;
      Password = password;
      KolegijId = kolegijId;
    }
    public int UserId { get; set; }
    public string Password { get; set; }
    public int KolegijId { get; set; }
  }

  public class KorisnikUnsubscribeCommand : ICommand
  {
    public KorisnikUnsubscribeCommand() { }
    public KorisnikUnsubscribeCommand(int userId, int kolegijId)
    {
      UserId = userId;
      KolegijId = kolegijId;
    }
    public int UserId { get; set; }
    public int KolegijId { get; set; }
  }

  public class KorisnikUpdateSettingsCommand : ICommand
  {
    public KorisnikUpdateSettingsCommand() { }
    public int UserId { get; set; }
    public bool DarkMode { get; set; }
    public string Locale { get; set; }
  }
}