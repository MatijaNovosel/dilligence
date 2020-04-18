using System;
using System.Collections.Generic;
using tvz2api_cqrs.Models;

namespace tvz2api_cqrs.QueryModels
{
  public class KorisnikQueryModel
  {
    public int Id { get; set; }
    public string Username { get; set; }
    public DateTime? Created { get; set; }
  }
  public class KorisnikChatQueryModel
  {
    public int Id { get; set; }
    public KorisnikQueryModel FirstParticipant { get; set; }
    public KorisnikQueryModel SecondParticipant { get; set; }
  }
  public class KorisnikSettingsQueryModel
  {
    public bool DarkMode { get; set; }
    public string Locale { get; set; }
  }
}
