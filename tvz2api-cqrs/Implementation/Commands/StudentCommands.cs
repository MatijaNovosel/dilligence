using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tvz2api_cqrs.Infrastructure.Commands;

namespace tvz2api_cqrs.Implementation.Commands
{
  public class StudentSubscribeCommand : ICommand
  {
    public StudentSubscribeCommand() { }
    public StudentSubscribeCommand(int studentId, string password, int kolegijId)
    {
      StudentId = studentId;
      Password = password;
      KolegijId = kolegijId;
    }
    public int StudentId { get; set; }
    public string Password { get; set; }
    public int KolegijId { get; set; }
  }

  public class StudentUnsubscribeCommand : ICommand
  {
    public StudentUnsubscribeCommand() { }
    public StudentUnsubscribeCommand(int studentId, int kolegijId)
    {
      StudentId = studentId;
      KolegijId = kolegijId;
    }
    public int StudentId { get; set; }
    public int KolegijId { get; set; }
  }
}