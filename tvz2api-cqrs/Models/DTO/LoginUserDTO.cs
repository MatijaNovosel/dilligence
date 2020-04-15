using System;
using System.Collections.Generic;
using tvz2api_cqrs.Models;
using tvz2api_cqrs.QueryModels;

namespace tvz2api_cqrs.Models.DTO
{
  public class LoginUserDTO
  {
    public LoginUserDTO(string token, string username, int id, KorisnikSettingsQueryModel settings)
    {
      Token = token;
      Username = username;
      Id = id;
      Settings = settings;
    }
    public LoginUserDTO() { }

    public string Token { get; set; }
    public string Username { get; set; }
    public List<int> Privileges { get; set; }
    public KorisnikSettingsQueryModel Settings { get; set; }
    public int Id { get; set; }
  }
}
