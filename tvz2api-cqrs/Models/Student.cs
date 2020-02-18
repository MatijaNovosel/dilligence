using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
  public partial class Student
  {
    public Student()
    {
      Pretplata = new HashSet<Pretplata>();
      StudentKolegij = new HashSet<StudentKolegij>();
    }

    public int Id { get; set; }
    public string Jmbag { get; set; }
    public string Ime { get; set; }
    public string Prezime { get; set; }
    public string ImagePath { get; set; }
    public string Email { get; set; }
    public int? SmjerId { get; set; }

    public virtual Smjer Smjer { get; set; }
    public virtual ICollection<Pretplata> Pretplata { get; set; }
    public virtual ICollection<StudentKolegij> StudentKolegij { get; set; }
  }
}
