using System;
using System.Collections.Generic;

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
    public int FirstParticipantId { get; set; }
    public int SecondParticipantId { get; set; }
  }
}
