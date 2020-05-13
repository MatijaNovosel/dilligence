using System;
using System.Collections.Generic;
using tvz2api_cqrs.Models;
using tvz2api_cqrs.QueryModels;

namespace tvz2api_cqrs.Models.DTO
{
  public class LoginUserDTO
  {
    public LoginUserDTO() { }
    public int Id { get; set; }
    public string Token { get; set; }
    public string Username { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Picture { get; set; }
    public UserPrivilegeDTO Privileges { get; set; }
    public string ImageBlob { get; set; }
    public UserSettingsQueryModel Settings { get; set; }
  }
}
