using tvz2api_cqrs.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace tvz2api_cqrs.Implementation.Commands
{
  public class AuthenticationRegisterCommand : ICommand
  {
    public AuthenticationRegisterCommand(string username, string password)
    {
      Username = username;
      Password = password;
    }
    public string Username { get; set; }
    public string Password { get; set; }
  }
}
