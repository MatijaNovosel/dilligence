using System;
using System.Collections.Generic;
using tvz2api_cqrs.Models;

namespace tvz2api_cqrs.QueryModels
{
  public class UserQueryModel
  {
    public int Id { get; set; }
    public string Username { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime? Created { get; set; }
    public string Email { get; set; }
    public string Picture { get; set; }
  }
  public class UserChatQueryModel
  {
    public int Id { get; set; }
    public UserQueryModel FirstParticipant { get; set; }
    public UserQueryModel SecondParticipant { get; set; }
  }
  public class UserSettingsQueryModel
  {
    public bool? DarkMode { get; set; }
    public string Locale { get; set; }
    public bool? Popups { get; set; }
  }
}
