using tvz2api_cqrs.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tvz2api_cqrs.Models.DTO;

namespace tvz2api_cqrs.Implementation.Commands
{
  public class AuthenticationRegisterCommand : ICommand
  {
    public AuthenticationRegisterCommand() { }
    public AuthenticationRegisterCommand(string username, string password)
    {
      Username = username;
      Password = password;
    }
    public string Username { get; set; }
    public string Password { get; set; }
  }

  public class AuthenticationLoginCommand : ICommand<LoginUserDTO>
  {
    public AuthenticationLoginCommand() { }
    public AuthenticationLoginCommand(string username, string password)
    {
      Username = username;
      Password = password;
    }
    public string Username { get; set; }
    public string Password { get; set; }
  }
}
