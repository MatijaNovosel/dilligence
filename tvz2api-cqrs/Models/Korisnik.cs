using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
  public partial class Korisnik
  {
    public Korisnik()
    {
      ChatFirstParticipant = new HashSet<Chat>();
      ChatSecondParticipant = new HashSet<Chat>();
      Exam = new HashSet<Exam>();
      Message = new HashSet<Message>();
      UserPrivileges = new HashSet<UserPrivileges>();
      UserSettings = new HashSet<UserSettings>();
    }

    public int Id { get; set; }
    public string Username { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public DateTime? Created { get; set; }

    public virtual ICollection<Chat> ChatFirstParticipant { get; set; }
    public virtual ICollection<Chat> ChatSecondParticipant { get; set; }
    public virtual ICollection<Exam> Exam { get; set; }
    public virtual ICollection<Message> Message { get; set; }
    public virtual ICollection<UserPrivileges> UserPrivileges { get; set; }
    public virtual ICollection<UserSettings> UserSettings { get; set; }
  }
}
