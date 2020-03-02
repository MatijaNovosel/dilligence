using System;
using System.Collections.Generic;
using tvz2api_cqrs.Models;

namespace tvz2api_cqrs.Models.DTO
{
  public class LoginUserDTO
  {
    public LoginUserDTO(string token, string username, int id)
    {
      Token = token;
      Username = username;
      Id = id;
    }
    public string Token { get; set; }
    public string Username { get; set; }
    public int Id { get; set; }
  }
}
